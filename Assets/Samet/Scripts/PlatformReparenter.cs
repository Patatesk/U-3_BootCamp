using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SametJR
{
    public class PlatformReparenter : NetworkBehaviour
    {
        private bool isPlayerOnPlatform = false;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log($"Platform - Collision with {other.gameObject.name}");
                // other.gameObject.transform.parent = transform;
                ReparentObjectServerRpc(other.gameObject.GetComponent<NetworkObject>().NetworkObjectId, GetComponent<NetworkObject>().NetworkObjectId);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.transform.parent == transform)
                    // other.gameObject.transform.parent = null;
                    ReparentObjectServerRpc(other.gameObject.GetComponent<NetworkObject>().NetworkObjectId, 999999);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void ReparentObjectServerRpc(ulong parentedObjectId, ulong parentObjectId)
        {
            // transform.parent = parent;
            var parentedObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[parentedObjectId].gameObject;
            if(parentObjectId == 999999)
            {
                parentedObject.transform.parent = null;
                return;
            }
            var parentObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[parentObjectId].gameObject;
            parentedObject.transform.parent = parentObject.transform;
            // parentedObject.transform.localScale /= parentObject.transform.localScale.x;
        }
    }
}
