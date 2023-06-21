using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;

namespace BootCamp.PK
{
    public class EventCatchBase : MonoBehaviour
    {
        [SerializeField] private int channel = 0;
        [SerializeField] private bool canCatch = true;
        [SerializeField] protected bool loop = false;
        [SerializeField] protected bool returnBack;
        [SerializeField] protected float returnTime = 3f;


        protected Vector3 startPos;

        private void Awake()
        {
            startPos = transform.position;
        }
        private void OnEnable()
        {
            if (!canCatch) return;
            TriggerEvent.StartEvent += CatchStartEvent;
            TriggerEvent.EndEvent += CatchEndEvent;
            
        }

        private void OnDisable()
        {
            if (!canCatch) return;
            TriggerEvent.StartEvent -= CatchStartEvent;
            TriggerEvent.EndEvent -= CatchEndEvent;
            
        }

        private void CatchStartEvent(int channel)
        {
            if (!canCatch) return;
            PerformStartEvent();
            
        }

        private void CatchEndEvent(int channel)
        {
            if (!canCatch) return;
            PerformEndEvent();
            
        }
        
        protected virtual void PerformStartEvent()
        {
            
        }

        protected virtual void PerformEndEvent()
        {
            
        }

        protected virtual void ReturnBack()
        {
            
        }
    }
}
