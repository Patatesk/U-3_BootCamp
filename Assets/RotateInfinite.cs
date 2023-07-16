using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class RotateInfinite : MonoBehaviour
    {

        [SerializeField] private Direction direction;
        [SerializeField] private float speed;

        private Vector3 rotationAxis;

        private void Start()
        {
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
        private void Update()
        {
            transform.Rotate(rotationAxis, 90f * Time.deltaTime * speed);
        }
        public enum Direction
        {
            X,
            Y,
            Z
        }
    }
}
