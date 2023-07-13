using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.PK
{
    public class Move : EventCatchBase 
    {
       protected override void PerformStartEvent(int channel)
        {
            if (loop)
            {
                transform.DOScale(50, 2f).SetLoops(50,LoopType.Yoyo);
                transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 3f).SetLoops(50, LoopType.Yoyo);
            }
        }
    }
}
