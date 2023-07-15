using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;
using Unity.Netcode;
using BootCamp.SametJR;

namespace SametJR
{
    // [RequireComponent(typeof(TriggerTest))]
    [RequireComponent(typeof(Collider))]
    public class ButtonTriggerFireEvent : TriggerTest
    {
        MeshRenderer mr;
        private GameObject onTop;
        private void Start() {
            mr = GetComponent<MeshRenderer>();
            Debug.Log($"Mesh renderer of child is {mr}");
            mr.material.color = Color.green;
            GetComponent<Collider>().isTrigger = true;       
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log($"Triggered by {other.name}");
            if (onTop != null) return;
            if((other.CompareTag("Player") && other.GetComponent<CharacterMovement>().canPush) || other.CompareTag("Pushable"))
            {
                Debug.Log($"Triggering event on channel {channel}");
                TriggerEventMethod(channel);
                if(onTop == null)
                onTop = other.gameObject;
            }
            mr.material.color = Color.red;
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject != onTop) return;
            onTop = null;
            mr.material.color = Color.green;
            TriggerEventMethod_End(channel);
        }

    }
}
