using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;

namespace SametJR
{
    public class MovingPlatformEventCatcher : EventCatchBase
    {
        private Vector3 initialPosition;
        [SerializeField] private Vector3 target;
        [SerializeField] private float time = 1f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
        [SerializeField] private bool isReturningBack = false;
        [SerializeField] private bool isPingPong = false;
        private void Start() {
            initialPosition = transform.position;
        }

        protected override void PerformStartEvent()
        {
            if (LeanTween.isTweening(gameObject)) return;

            if (isPingPong)
            {
                LeanTween.moveLocal(gameObject, initialPosition, time).setEase(easeType).setLoopPingPong();
                return;
            }
            LeanTween.moveLocal(gameObject, target, time).setEase(easeType);
            
            if (isReturningBack)
            {
                LeanTween.moveLocal(gameObject, initialPosition, time).setEase(easeType).setDelay(time);
            }
            
        }

    }
}
