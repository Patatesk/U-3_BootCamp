using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BootCamp.PK
{
    public class TriggerTest : MonoBehaviour
    {
        [SerializeField] private int channel = 0;
        [SerializeField] private bool canTrigger = true;
        [ContextMenu("Trigger")]
        public void Test()
        {
            TriggerEvent.Trigger_Start(channel);
        }
        public void TriggerEventMethod(int channel)
        {
            if (!canTrigger) return;
            TriggerEvent.Trigger_Start(channel);
        }

        public void TriggerEventMethod_End(int channel)
        {
            if (!canTrigger) return;
            TriggerEvent.Trigger_End(channel);
        }
    }


    

    public static class TriggerEvent
    {
        public static event Action<int> StartEvent;
        public static void Trigger_Start(int channel)
        {
            StartEvent?.Invoke(channel);
        }
        public static event Action<int> EndEvent;
        public static void Trigger_End(int channel)
        {
            EndEvent?.Invoke(channel);
        }
    }
}
