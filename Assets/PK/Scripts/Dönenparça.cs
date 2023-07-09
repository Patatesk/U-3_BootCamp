using BootCamp.PK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SametJR
{
    public class Dönenparça : EventCatchBase
    {
        override protected void PerformStartEvent()
        {
            transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}
