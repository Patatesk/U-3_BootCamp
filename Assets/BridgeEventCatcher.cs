using BootCamp.PK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class BridgeEventCatcher : EventCatchBase
    {
        [SerializeField] private Transform target;


        private Vector3 initialPosition;


        private void Awake()
        {
            initialPosition = transform.position;
        }
        protected override void PerformStartEvent(int _channel)
        {
            if(target != null)
            {
                LeanTween.cancel(gameObject);
                LeanTween.move(gameObject, target.position, .5f);
            }
        }

        protected override void PerformEndEvent(int _channel)
        {
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, initialPosition, .5f);
        }
    }
}
