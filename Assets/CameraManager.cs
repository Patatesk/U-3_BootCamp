using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class CameraManager : MonoBehaviour
    {
        public Vector3 offset;
        public GameObject currentPlatformLevel;

        public float multiplier = 1.1f;
        private void Start() {
            offset = transform.position - currentPlatformLevel.transform.position;
        }

        public void UpdateCameraPosition() {
            // Arrange the camera such that camera gets further away from platform 
            // as the platform gets higher
            transform.position = currentPlatformLevel.transform.position + offset;
            // move camera further
            offset = (transform.position - currentPlatformLevel.transform.position) * multiplier;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                UpdateCameraPosition();
            }
        }
    }
}
