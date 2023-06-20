using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

namespace BootCamp.SametJR
{
    public class CharacterController : NetworkBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float pushingSpeed = 2f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float pushRange = 2f;
        [SerializeField] private float pushForce = 25f;

        private Vector3 movement;
        private bool isPushing = false;
        private bool canJump = false;
        private bool canPush = false;
        private bool canMove = true;
        private Rigidbody rb;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner) return;
            if (OwnerClientId == 1) canJump = true;
            if (OwnerClientId == 0) canPush = true;
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            // If this is not the local player, do nothing
            if (!IsLocalPlayer)
            {
                return;
            }


            CheckMovement();
            canMove = true;
            isPushing = false;
            CheckForPushables();



        }

        private void CheckForPushables()
        {
            if(!canPush)
            {
                canMove = false;
                return;
            } 
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position - Vector3.up, transform.forward, Color.red);
            Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.red);


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pushRange) ||
                Physics.Raycast(transform.position - Vector3.up, transform.forward, out hit, pushRange) ||
                Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, pushRange))

            {
                if (hit.collider.gameObject.CompareTag("pushable"))
                {
                    Debug.Log("Pushing");
                    isPushing = true;
                    hit.collider.gameObject.transform.position += movement * pushingSpeed * Time.deltaTime;
                }
            }
        }

        private void CheckMovement()
        {
            if (!canMove) return;
            // Get the input from the player
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Move the player
            movement = new Vector3(horizontal, 0, vertical);
            var sped = isPushing ? pushingSpeed : speed;
            Debug.Log(sped);
            transform.position += movement * sped * Time.deltaTime;

            // Rotate the player to face the direction of movement
            if (movement != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(movement);

            // If the player presses the space bar, jump
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
