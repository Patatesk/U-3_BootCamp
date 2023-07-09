using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;

namespace SametJR
{
    [RequireComponent(typeof(TriggerTest))]
    [RequireComponent(typeof(Collider))]
    public class LeverTriggerFireEvent : TriggerTest
    {
        GameObject child;
        private void Start() {
            GetComponent<Collider>().isTrigger = true;       
            child = transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log($"Triggered by {other.name}");
            if(other.CompareTag("Player"))
            {
                Debug.Log($"Triggering event on channel {channel}");
                Test();
            }
            LeanTween.rotateLocal(child, new Vector3(0, 0, 45), 1f).setEaseInOutSine();
        }

        private void OnTriggerExit(Collider other) {
            LeanTween.rotateLocal(child, new Vector3(0, 0, -45), 1f).setEaseInOutSine();
        }

    }
}
