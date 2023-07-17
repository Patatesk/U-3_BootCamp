using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class LevelChange : MonoBehaviour
    {
       

        private bool playerOneReady = false;
        private bool playerTwoReady = false;
        private int totalPlayerInside = 0;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                totalPlayerInside++;

                if (totalPlayerInside == 2)
                {
                    LevelManager1.Instance.ChangeLevel();
                } 
            }
        }
      
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                totalPlayerInside--;
            }
        }
    }
}
