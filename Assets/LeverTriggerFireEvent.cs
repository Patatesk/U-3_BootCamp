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
        private State lastState = State.Close;
        private void Start() {
            GetComponent<Collider>().isTrigger = true;       
            child = transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other) {

            if(other.CompareTag("Player") && lastState == State.Close)
            {
                TriggerEventMethod(channel);
                lastState = State.Open;
                LeanTween.rotateLocal(child, new Vector3(0, 0, 45), .5f);

            }
            else if (other.CompareTag("Player") && lastState == State.Open)
            {
                TriggerEventMethod_End(channel);
                lastState = State.Close;
                Debug.Log("Closing");
                LeanTween.rotateLocal(child, new Vector3(0, 0, -45), .5f);

            }
        }

      

        enum State
        {
            Open,
            Close
        }
    }
}
