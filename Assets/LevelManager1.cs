using System.Collections;
using System.Collections.Generic;
using BootCamp.SametJR;
using UnityEngine;

namespace SametJR
{
    public class LevelManager1 : MonoBehaviour
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
        private Transform[] spawnPointsForBigOne;
        private Transform[] spawnPointsForSmallOne;
        private void Start()
        {
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
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                CharacterMovement cm = player.GetComponent<CharacterMovement>();
                if (!cm.canJump)
                {
                    // player.transform.position = spawnPointsForBigOne[0].position;
                    cm.SetTransform(spawnPointsForBigOne[level]);
                }
                else
                {
                    // player.transform.position = spawnPointsForSmallOne[0].position;
                    cm.SetTransform(spawnPointsForSmallOne[level]);
                }
            }
            if (level >= levels.Length)
            {
                level = 0;
            }
            levels[level].SetActive(true);
        }





    }
}
