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
                Debug.Log(" ========================== Total player inside: " + totalPlayerInside);

                if (totalPlayerInside == 2)
                {
                    Debug.Log(" ======================= === Calling ChangeLevel() === ========================");
                    StartCoroutine(WaitForServerToTick());
                }
            }
        }

        private IEnumerator WaitForServerToTick()
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(" ======================= === Calling ChangeLevel() === ========================");
            GameObject.FindObjectOfType<LevelManager1>().ChangeLevel();

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
