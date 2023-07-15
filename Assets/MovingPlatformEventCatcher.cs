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
        [SerializeField] private int setChannel = 999;


        bool infiniteLoop = false;

        private void Start()
        {
            initialPosition = transform.localPosition;

        }

        protected override void PerformStartEvent(int _channel)
        {
            if (_channel == setChannel)
            {
                infiniteLoop = true;
                return;
            }
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
        protected override void PerformEndEvent(int _channel)
        {
            if(!infiniteLoop)
            StopTweening();
            
        }
        public void StopTweening()
        {
            LeanTween.cancel(gameObject);
        }
        private bool IsEven(int number) => number % 2 == 0;
    }


}
