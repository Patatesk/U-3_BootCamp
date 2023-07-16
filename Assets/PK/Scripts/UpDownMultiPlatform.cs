using BootCamp.PK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class UpDownMultiPlatform : EventCatchBase
    {
        [SerializeField] private Transform target;
        private Vector3 initialPosition;
        private LastState lastState = LastState.Down;

        private void Start()
        {
            initialPosition = transform.position;
        }
        protected override void PerformStartEvent(int _channel)
        {
            if (lastState == LastState.Up)
            {
                LeanTween.cancel(gameObject);
                LeanTween.moveY(gameObject, initialPosition.y, .5f);

                lastState = LastState.Down;
                return;

            }
            else if (lastState == LastState.Down)
            {
                LeanTween.cancel(gameObject);
                LeanTween.moveY(gameObject, target.position.y, .5f);

                lastState = LastState.Up;
                return;

            }
        }


        public enum LastState
        {
            Up,
            Down
        }
    }
}
