using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.Hamza
{
    [ExecuteInEditMode]
    public class PoolPlatform : MonoBehaviour
    {
        public float movementUnit = 5f;
        public Vector3 movementDirection;
        public LayerMask obstacleLayer;

        private Vector3 positionA;
        private Vector3 positionB;
        private bool isMovingToPositionA = false;
        [HideInInspector] public bool canMove;

        private void Start()
        {
            positionA = transform.position;
            positionB = positionA + movementDirection * movementUnit;

            Vector3 target = isMovingToPositionA ? positionA : positionB;
            IsObstacleInPath(target);
        }

        [ContextMenu("Move")]
        public void MovePlatform()
        {
            Vector3 targetPosition = isMovingToPositionA ? positionA : positionB;

            if (!IsObstacleInPath(targetPosition))
            {
                StartCoroutine(MoveToPosition(targetPosition));
            }
            else
            {
                Debug.Log("Can't move platform.");
            }
        }


        private bool IsObstacleInPath(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - transform.position;
            Vector3 raycastOffset = Vector3.Cross(direction.normalized, Vector3.up) * 1f;

            bool obstacleDetected = Physics.Raycast(transform.position + raycastOffset, direction, movementUnit + 1, obstacleLayer)
                || Physics.Raycast(transform.position - raycastOffset, direction, movementUnit + 1, obstacleLayer);

            if (obstacleDetected)
            {
                canMove = false;
                return true;
            }
            canMove = true;
            return false;
        }

        private IEnumerator MoveToPosition(Vector3 targetPosition)
        {
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
                yield return null;
            }

            isMovingToPositionA = !isMovingToPositionA;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 targetPosition = isMovingToPositionA ? positionA : positionB;
            Vector3 direction = targetPosition - transform.position;
            Vector3 raycastOffset = Vector3.Cross(direction.normalized, Vector3.up) * 1f;

            Gizmos.DrawRay(transform.position + raycastOffset, direction.normalized * (movementUnit + 1));
            Gizmos.DrawRay(transform.position - raycastOffset, direction.normalized * (movementUnit + 1));
        }
    }
}
