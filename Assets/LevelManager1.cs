using System.Collections;
using System.Collections.Generic;
using BootCamp.SametJR;
using UnityEngine;
using Unity.Netcode;
using System;

namespace SametJR
{
    public class LevelManager1 : NetworkBehaviour
    {
        

        #region Singleton
        public static LevelManager1 Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There is more than one LevelManager1 in the scene!");
                Destroy(this);
            }
        }
        #endregion
        
        [SerializeField] private GameObject[] levels;
        private int level = 0;
        [SerializeField] private GameObject SpawnPointForBigOne;
        [SerializeField] private GameObject SpawnPointForSmallOne;
        private Transform[] spawnPointsForBigOne = new Transform[5];
        private Transform[] spawnPointsForSmallOne = new Transform[5];
        private void Start()
        {
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i].SetActive(false);
            }

            int index = 0;

            foreach (Transform t in SpawnPointForBigOne.transform)
            {
                spawnPointsForBigOne[index] = t;
                index++;
            }
            index = 0;
            foreach (Transform t in SpawnPointForSmallOne.transform)
            {
                spawnPointsForSmallOne[index] = t;
                index++;
            }
        }

        public void ChangeLevel()
        {
            levels[level].SetActive(false);
            level++;
            ChangePlayerPositionsServerRPC();
            Debug.Log("Changing level -------------------------------------");
            if (level >= levels.Length)
            {
                level = 0;
            }
            levels[level].SetActive(true);
        }

        // foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        //     {
        //         CharacterMovement cm = player.GetComponent<CharacterMovement>();
        //         if (!cm.canJump)
        //         {
        //             // player.transform.position = spawnPointsForBigOne[0].position;
        //             cm.SetTransform(spawnPointsForBigOne[level]);
        //         }
        //         else
        //         {
        //             // player.transform.position = spawnPointsForSmallOne[0].position;
        //             cm.SetTransform(spawnPointsForSmallOne[level]);
        //         }
        //     }

        [ServerRpc(RequireOwnership = false)]
        public void ChangePlayerPositionsServerRPC()
        {
            // if(!IsHost) return;

            // Debug.Log($"<color='#ffffff'GameObjects length: {GameObjects.Length} -- level: {level} -- Names: {GameObjects[0].name} - {GameObjects[1].name}");
            ChangePlayerPosClientRPC();   
        }

        [ClientRpc]
        private void ChangePlayerPosClientRPC()
        {
            var GameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in GameObjects)
            {
                CharacterMovement cm = player.GetComponent<CharacterMovement>();
                if (!cm.canJump)
                {
                    // player.transform.position = spawnPointsForBigOne[0].position;
                    cm.SetTransform(spawnPointsForBigOne[level]);
                }
                
                if (cm.canJump)
                {
                    // player.transform.position = spawnPointsForSmallOne[0].position;
                    cm.SetTransform(spawnPointsForSmallOne[level]);
                }
            }
        }
    }
}
