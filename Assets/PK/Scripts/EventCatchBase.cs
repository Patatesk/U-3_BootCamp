using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UIElements;

namespace BootCamp.PK
{
    public class EventCatchBase : MonoBehaviour
    {
        [SerializeField] protected int[] channels;
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
            if(channels.Contains(channel))
            PerformStartEvent(channel);
            
        }

        private void CatchEndEvent(int channel)
        {
            if (!canCatch) return;
            if (channels.Contains(channel))
                PerformEndEvent(channel);
            
        }
        
        protected virtual void PerformStartEvent(int _channel)
        {
            
        }

        protected virtual void PerformEndEvent(int _channel)
        {
            
        }

        protected virtual void ReturnBack()
        {
            
        }
    }
}
