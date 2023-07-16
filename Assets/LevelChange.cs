using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class LevelChange : MonoBehaviour
    {
        [SerializeField] private GameObject[] levels;
        private int level = 0;

        private bool playerOneReady = false;
        private bool playerTwoReady = false;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!playerOneReady)
                {
                    playerTwoReady = true;
                }
                else if (!playerTwoReady)
                {
                    playerOneReady = true;
                }

                if (playerTwoReady && playerOneReady) ChangeLevel();
            }
        }
        private void ChangeLevel()
        {
            levels[level].SetActive(false);
            level++;
            if (level >= levels.Length)
            {
                level = 0;
            }
            levels[level].SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (playerOneReady)
                {
                    playerOneReady = false;
                }
                else if (playerTwoReady)
                {
                    playerTwoReady = false;
                }
            }
        }
    }
}
