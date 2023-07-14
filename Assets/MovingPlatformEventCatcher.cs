using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;

namespace SametJR
{
    public class MovingPlatformEventCatcher : EventCatchBase
    {
        private Vector3 initialPosition;
        [SerializeField] private List<Transform> targets;
        [SerializeField] private float time = 1f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;
        [SerializeField] private bool isReturningBack = false;
        [SerializeField] private bool isPingPong = false;
        private void Start()
        {
            initialPosition = transform.localPosition;

        }

        protected override void PerformStartEvent(int _channel)
        {
            Vector3 target = initialPosition;
            if (targets.Count == 1)
            {
                target = targets[0].localPosition;
                Debug.Log("Target is " + target);
            }

            else
            {
                target = targets[IsEven(_channel) ? 0 : 1].localPosition;

            }



            // match up the indexes of the channels and targets






            if (LeanTween.isTweening(gameObject)) return;

            if (isPingPong)
            {   
                Debug.Log($"Ping ponging to local position {target}");
                LeanTween.moveLocal(gameObject, target, time).setEase(easeType).setLoopPingPong();
                return;
            }
            LeanTween.moveLocal(gameObject, target, time).setEase(easeType);

            if (isReturningBack)
            {
                LeanTween.moveLocal(gameObject, initialPosition, time).setEase(easeType).setDelay(time);
            }

        }
        private bool IsEven(int number) => number % 2 == 0;
    }


}
