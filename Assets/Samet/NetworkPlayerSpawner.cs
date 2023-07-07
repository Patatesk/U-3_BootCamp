using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace BootCamp.SametJR
{
    public class NetworkPlayerSpawner : MonoBehaviour
    {
        public NetworkVariable<int> playerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public GameObject bigPlayerPrefab, smallPlayerPrefab;
        public Vector3 spawnPoint1, spawnPoint2;

        private void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        }

        private void OnDestroy()
        {
            if (NetworkManager.Singleton == null) return;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
        }

        private void OnClientConnected(ulong clientId)
        {
            // we should not run this code twice (once for the server and once for the client)
            if (!NetworkManager.Singleton.IsServer) return;
            Debug.Log("Client connected" + clientId + " ---- " + playerCount.Value);
            playerCount.Value++;
            SpawnPlayerServerRpc(clientId);
        }

        private void OnClientDisconnect(ulong clientId)
        {
            playerCount.Value--;
        }



        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayerServerRpc(ulong clientId)
        {
            if (playerCount.Value == 1)
            {
                GameObject player = Instantiate(bigPlayerPrefab);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
                player.SetActive(false);
                player.transform.position = spawnPoint1;
                player.SetActive(true);
            }
            else if (playerCount.Value == 2)
            {
                GameObject player = Instantiate(smallPlayerPrefab, spawnPoint2, Quaternion.identity);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
                player.SetActive(false);
                player.transform.position = spawnPoint2;
                player.SetActive(true);
            }
        }


        

    }
}
