using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class LevelManager1 : MonoBehaviour
    {
        [SerializeField] private GameObject[] levels;
        private int level = 0;

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
        public void ChangeLevel()
        {
            levels[level].SetActive(false);
            level++;
            if (level >= levels.Length)
            {
                level = 0;
            }
            levels[level].SetActive(true);
        }

    }
}
