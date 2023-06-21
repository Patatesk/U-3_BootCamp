using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.PK
{
    public class EventCatchBase : MonoBehaviour
    {
        [SerializeField] private int channel = 0;
        [SerializeField] private bool canCatch = true;
        

        private void OnEnable()
        {
            if (!canCatch) return;
            
        }

    }
}
