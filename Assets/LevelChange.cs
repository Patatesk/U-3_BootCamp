using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class LevelChange : MonoBehaviour
    {
       

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

                if (playerTwoReady && playerOneReady) LevelManager1.Instance.ChangeLevel();
            }
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
