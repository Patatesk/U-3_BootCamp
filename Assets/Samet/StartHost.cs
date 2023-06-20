using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace BootCamp.SametJR
{
    public class StartHost : MonoBehaviour
    {
        [SerializeField] private Button startHostButton;
        private void Start() {
            startHostButton.onClick.AddListener(
                () => {
                    NetworkManager.Singleton.StartHost();
                }
            );
        }
    }
}
