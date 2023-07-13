using System.Collections;
using System.Collections.Generic;
using BootCamp.PK;
using UnityEngine;

namespace SametJR
{
    public class RotatingFenceEventCather : EventCatchBase
    {
        [SerializeField] private Direction direction;
        private Vector3 rotationAxis;
        private void Start() {
            switch (direction)
            {
                case Direction.X:
                    rotationAxis = Vector3.right;
                    break;
                case Direction.Y:
                    rotationAxis = Vector3.up;
                    break;
                case Direction.Z:
                    rotationAxis = Vector3.forward;
                    break;
                default:
                    break;
            }
        }
        protected override void PerformStartEvent(int _channel)
        {
            if (LeanTween.isTweening(gameObject)) return;
            LeanTween.rotateAround(gameObject, rotationAxis, 90f, 1.5f).setEaseInOutSine();
        }

        enum Direction
        {
            X,
            Y,
            Z
        }
    }
}
