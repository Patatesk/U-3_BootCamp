using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

namespace BootCamp.SametJR
{
    public class CharacterMovement : NetworkBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float pushingSpeed = 2f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float pushRange = 2f;
        [SerializeField] private float pushForce = 25f;

        public Vector3 movement;
        public bool isPushing = false;
        public bool canJump = false;
        public bool canPush = false;
        public bool canMove = true;
        public Rigidbody rb;
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
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.DrawRay(transform.position - Vector3.up, transform.forward, Color.red);
            Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.red);


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pushRange) ||
                Physics.Raycast(transform.position - Vector3.up, transform.forward, out hit, pushRange) ||
                Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, pushRange))

            {
                if (hit.collider.gameObject.CompareTag("Pushable"))
                {
                    Debug.Log("Pushing");
                    if(!canPush)
                    {
                        canMove = false;
                        return;
                    }
                    isPushing = true;
                    hit.collider.gameObject.transform.position += movement * pushingSpeed * Time.deltaTime;
                }
            }
        }

        private void CheckMovement()
        {
            
            // Get the input from the player
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Move the player
            movement = new Vector3(horizontal, 0, vertical);
            var _speed = isPushing ? pushingSpeed : speed;
            // Debug.Log(sped);

            if (canMove)
                transform.position += movement * _speed * Time.deltaTime;

            // Rotate the player to face the direction of movement
            if (movement != Vector3.zero)
                // transform.rotation = Quaternion.LookRotation(movement); //Instead of turning instantly, we can use Quaternion.Lerp to turn smoothly
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 0.2f);


            // If the player presses the space bar, jump
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!IsOwner) return;
            if(canJump) return;
            if(OwnerClientId == 0) return;
            if (collision.gameObject.CompareTag("Ground"))
            {
                canJump = true;
            }
        }
    }
}
