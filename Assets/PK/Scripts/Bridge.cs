using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.PK
{
    public class Bridge : EventCatchBase
    {
        protected override void PerformStartEvent()
        {
            transform.DOMove(transform.position + (Vector3.one * 2), 2);
            if (returnBack)
            {
                ReturnBack();
            }
        }


        protected override void ReturnBack()
        {
           transform.DOMove(startPos, 2).SetDelay(returnTime);
        }
    }
}
