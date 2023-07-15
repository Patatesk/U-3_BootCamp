using BootCamp.PK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class InfiniteMovingPlatform : EventCatchBase
    {
        private Vector3 initialPosition;
        [SerializeField] private List<Transform> targets;
        [SerializeField] private float time = 1f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutCirc;
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

            if (infiniteLoop) return;
            if (isPingPong)
            {
                
                LeanTween.move(gameObject, targets[0].position, time).setEase(easeType).setOnComplete(() =>
                LeanTween.move(gameObject, targets[1].position, time).setEase(easeType).setLoopPingPong());
            }
        }
        protected override void PerformEndEvent(int _channel)
        {
            if (!infiniteLoop)
                StopTweening();

        }
        public void StopTweening()
        {
            LeanTween.cancel(gameObject);
        }
    }
}
