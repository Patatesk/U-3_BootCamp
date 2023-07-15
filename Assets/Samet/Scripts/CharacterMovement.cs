using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
// using MLAPI;

namespace BootCamp.SametJR
{
    public class CharacterMovement : NetworkBehaviour
    {
        // Character data
        public Vector3 startPosition;
        public Vector3 initialScale;

        // Serialized variables
        [SerializeField] private float speed = 5f;
        [SerializeField] private float pushingSpeed = 2f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float pushRange = 2f;
        [SerializeField] private float pushForce = 25f;

        // Animator Hashes and references
        private Animator _animator;
        private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
        private readonly int _isPushingHash = Animator.StringToHash("isPushing");

        // Movement variables
        public Vector3 movement;
        public bool isPushing = false;
        public bool isMoving = false;
        public bool canJump = false;
        public bool canPush = false;
        public bool canMove = true;
        public bool isOnPlatform = false;
        public Rigidbody rb;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner) return;
            if (OwnerClientId == 1) canJump = true;
            if (OwnerClientId == 0) canPush = true;
            initialScale = transform.localScale;
            rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        public void SetStartPosition(Vector3 position)
        {
            if (IsOwner)
                startPosition = position;
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
            UpdateAnimations();
            transform.localScale = initialScale;


        }

        private void UpdateAnimations()
        {
            if (_animator == null)
            {
                Debug.Log($"Animator is null for {gameObject.name}");
                return;
            }

            if (isPushing)
            {
                _animator.SetBool(_isPushingHash, true);
                _animator.SetBool(_isWalkingHash, false);
            }
            else if (movement != Vector3.zero)
            {
                _animator.SetBool(_isWalkingHash, true);
                _animator.SetBool(_isPushingHash, false);
            }
            else
            {
                _animator.SetBool(_isWalkingHash, false);
                _animator.SetBool(_isPushingHash, false);
            }
        }

        private void CheckForPushables()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red); //Draws a ray in the scene view to show the direction of the raycast
            // Debug.DrawRay(transform.position - Vector3.up, transform.forward, Color.red);
            // Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.red);


            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, pushRange))

            {
                GameObject hitObject = hit.collider.gameObject;
                // Debug.Log($"Hit {hitObject.name}");
                if (hitObject.CompareTag("Pushable"))
                {
                    Debug.Log("Pushing");
                    if (!canPush)
                    {
                        canMove = false;
                        return;
                    }
                    isPushing = true;
                    hitObject.transform.position += movement * pushingSpeed * Time.deltaTime;
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
            {
                // if (transform.parent != null) /*transform.parent = null*/ /*ReparentObjectServerRpc(null)*/ ReparentObjectServerRpc(GetComponent<NetworkObject>().NetworkObjectId, 999999);

                // transform.rotation = Quaternion.LookRotation(movement); //Instead of turning instantly, we can use Quaternion.Lerp to turn smoothly
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 0.2f);
                isMoving = true;
            }


            // If the player presses the space bar, jump
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (!IsOwner) return;

            Debug.Log($"Collision with {collision.gameObject.name}");

            if (collision.gameObject.CompareTag("Ground"))
            {
                if (canJump) return;
                if (OwnerClientId == 0) return;
                canJump = true;
            }

            if (collision.gameObject.CompareTag("DeadZone"))
            {
                Debug.Log("Deadzone");
                transform.position = startPosition;
            }

            if (collision.gameObject.CompareTag("Platform"))
            {
                Debug.Log($"Big one collided with platform {collision.gameObject.name}");
                // transform.parent = collision.gameObject.transform;
                // ReparentObjectServerRpc(collision.gameObject.transform);
                // ReparentObjectServerRpc(GetComponent<NetworkObject>().NetworkObjectId, collision.gameObject.GetComponent<NetworkObject>().NetworkObjectId);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                // if (transform.parent == null)
                    // transform.parent = other.gameObject.transform;
                    // ReparentObjectServerRpc(other.gameObject.transform);
                    // ReparentObjectServerRpc(GetComponent<NetworkObject>().NetworkObjectId, other.gameObject.GetComponent<NetworkObject>().NetworkObjectId);
                    // ;
            }
        }

        // [ServerRpc(RequireOwnership = false)]
        // public void ReparentObjectServerRpc(ulong parentedObjectId, ulong parentObjectId)
        // {
        //     // transform.parent = parent;
        //     var parentedObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[parentedObjectId].gameObject;
        //     if(parentObjectId == 999999)
        //     {
        //         parentedObject.transform.parent = null;
        //         return;
        //     }
        //     var parentObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[parentObjectId].gameObject;
        //     parentedObject.transform.parent = parentObject.transform;
        //     // parentedObject.transform.localScale /= parentObject.transform.localScale.x;
        // }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Trigger with {other.gameObject.name}");
        }
    }
}
