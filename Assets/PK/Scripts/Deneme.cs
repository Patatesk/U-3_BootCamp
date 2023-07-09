using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SametJR
{
    public class Deneme : MonoBehaviour
    {
        
       
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                
                NetworkManager.Singleton.StartHost();
            }
        }
    }
}
