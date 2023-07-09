using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;
using Unity.Netcode;
using BootCamp.SametJR;

namespace SametJR
{
    [RequireComponent(typeof(TriggerTest))]
    [RequireComponent(typeof(Collider))]
    public class ButtonTriggerFireEvent : TriggerTest
    {
        MeshRenderer meshRendererOfChild;
        private void Start() {
            meshRendererOfChild = transform.GetChild(0).GetComponent<MeshRenderer>();
            meshRendererOfChild.material.color = Color.green;
            GetComponent<Collider>().isTrigger = true;       
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log($"Triggered by {other.name}");
            if(other.CompareTag("Player") && other.GetComponent<CharacterMovement>().canPush)
            {
                Debug.Log($"Triggering event on channel {channel}");
                Test();
            }
            meshRendererOfChild.material.color = Color.red;
        }

        private void OnTriggerExit(Collider other) {
            meshRendererOfChild.material.color = Color.green;
        }

    }
}
