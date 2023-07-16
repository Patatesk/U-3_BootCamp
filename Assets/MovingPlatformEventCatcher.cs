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
        [SerializeField] private bool isMovingDown = false;


        bool infiniteLoop = false;

        private void Start()
        {
            initialPosition = transform.localPosition;

        }

        protected override void PerformStartEvent(int _channel)
        {
            LeanTween.cancel(gameObject);
            if (_channel == setChannel)
            {
                infiniteLoop = true;
                return;
            }
            Vector3 target = initialPosition;
            if (targets.Count == 1)
            {
                if (isMovingDown) {
                    LeanTween.moveY(gameObject, targets[0].position.y,.5f).setOnComplete(()=> transform.GetComponent<BoxCollider>().enabled = false);
                    return;
                }
                target = targets[0].position;
                Debug.Log("Target is " + target);
            }

            else
            {
                target = targets[IsEven(_channel) ? 0 : 1].position;

            }



            // match up the indexes of the channels and targets






            if (LeanTween.isTweening(gameObject)) return;

            if (isPingPong)
            {
                Debug.Log($"Ping ponging to local position {target}");
                LeanTween.move
                    (gameObject, target, time).setEase(easeType).setLoopPingPong();
                return;
            }
            LeanTween.move(gameObject, target, time).setEase(easeType);

            if (isReturningBack)
            {
                LeanTween.move(gameObject, initialPosition, time).setEase(easeType).setDelay(time);
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
