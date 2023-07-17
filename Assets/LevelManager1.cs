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


        // #region Singleton
        // public static LevelManager1 Instance { get; private set; }
        // private void Awake()
        // {
        //     if (Instance == null)
        //     {
        //         Instance = this;
        //     }
        //     else
        //     {
        //         Debug.LogError("There is more than one LevelManager1 in the scene!");
        //         Destroy(this);
        //     }
        // }
        // #endregion

        [SerializeField] private GameObject[] levels;
        private int level = 0;
        // [SerializeField] private GameObject SpawnPointForBigOne;
        // [SerializeField] private GameObject SpawnPointForSmallOne;
        // private Vector3[] spawnPointsForBigOne = new Vector3[5];
        // private Vector3[] spawnPointsForSmallOne = new Vector3[5];
        [SerializeField]
        private Transform b1, b2, b3, b4, b5
            , s1, s2, s3, s4, s5;

        private List<Transform> spawnPointsForBigOne = new List<Transform>();
        private List<Transform> spawnPointsForSmallOne = new List<Transform>();
        private void Start()
        {
            spawnPointsForBigOne.Add(b1);
            spawnPointsForBigOne.Add(b2);
            spawnPointsForBigOne.Add(b3);
            spawnPointsForBigOne.Add(b4);
            spawnPointsForBigOne.Add(b5);

            spawnPointsForSmallOne.Add(s1);
            spawnPointsForSmallOne.Add(s2);
            spawnPointsForSmallOne.Add(s3);
            spawnPointsForSmallOne.Add(s4);
            spawnPointsForSmallOne.Add(s5);

            for (int i = 1; i < levels.Length; i++)
            {
                levels[i].SetActive(false);
            }

            // int index = 0;

            // foreach (Transform t in SpawnPointForBigOne.transform)
            // {
            //     spawnPointsForBigOne[index] = t.TransformPoint(t.position);
            //     index++;
            // }
            // index = 0;
            // foreach (Transform t in SpawnPointForSmallOne.transform)
            // {
            //     spawnPointsForSmallOne[index] = t.TransformPoint(t.position);
            //     index++;
            // }
        }

        public void ChangeLevel()
        {
            // if(!IsOwner) return;
            Debug.Log("Before deactivating first level -------------------------------------");
            levels[level].SetActive(false);
            Debug.Log("After deactivating first level -------------------------------------");
            level++;
            if (level >= levels.Length)
            {
                level = 0;
            }
            levels[level].SetActive(true);
            // ChangePlayerPositionsServerRPC();

            // Debug.Log($"Change position to -> {spawnPointsForBigOne[level]}");
            if (IsHost)
            {
                GameObject bigPlayer = GameObject.Find("BigPlayer");
                CharacterMovement cm = bigPlayer.GetComponent<CharacterMovement>();
                cm.SetStartPosition(spawnPointsForBigOne[level].position);
                cm.SetTransform(spawnPointsForBigOne[level]);
                Debug.Log($"Change position for big one to -> {spawnPointsForBigOne[level].position}");
            }
            else
            {
                GameObject smallPlayer = GameObject.Find("SmallBoiPrefab(Clone)");
                CharacterMovement cm = smallPlayer.GetComponent<CharacterMovement>();
                cm.SetStartPosition(spawnPointsForSmallOne[level].position);
                cm.SetTransform(spawnPointsForSmallOne[level]);
                Debug.Log($"Change position for small one to -> {spawnPointsForSmallOne[level].position}");
            }

            Debug.Log("Changing level -------------------------------------");

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

        // [ServerRpc(RequireOwnership = false)]
        // public void ChangePlayerPositionsServerRPC()
        // {
        //     // if(!IsHost) return;

        //     // Debug.Log($"<color='#ffffff'GameObjects length: {GameObjects.Length} -- level: {level} -- Names: {GameObjects[0].name} - {GameObjects[1].name}");
        //     ChangePlayerPosClientRPC();   
        // }

        // [ClientRpc]
        // private void ChangePlayerPosClientRPC()
        // {
        //     var GameObjects = GameObject.FindGameObjectsWithTag("Player");
        //     foreach (GameObject player in GameObjects)
        //     {
        //         CharacterMovement cm = player.GetComponent<CharacterMovement>();
        //         if (!cm.canJump)
        //         {
        //             // player.transform.position = spawnPointsForBigOne[0].position;
        //             cm.SetTransform(spawnPointsForBigOne[level]);
        //         }

        //         if (cm.canJump)
        //         {
        //             // player.transform.position = spawnPointsForSmallOne[0].position;
        //             cm.SetTransform(spawnPointsForSmallOne[level]);
        //         }
        //     }
        // }
    }
}
