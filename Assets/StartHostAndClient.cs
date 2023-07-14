using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace SametJR
{
    public class StartHostAndClient : MonoBehaviour
    {
        public Button hostButton, clientButton;

        private void Start()
        {
            hostButton.onClick.AddListener(StartHost);
            clientButton.onClick.AddListener(StartClient);
        }

        private void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        private void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
