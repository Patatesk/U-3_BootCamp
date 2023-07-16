using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using System;

namespace SametJR
{
    public class RelayUI : NetworkBehaviour
    {  
        [SerializeField] private TMP_InputField joinCodeInputField;
        [SerializeField] private Button joinButton;
        [SerializeField] private Button hostButton;
        [SerializeField] private TextMeshProUGUI joinCodeText;
        [SerializeField] private TextMeshProUGUI waitingForSecondPlayerText;

        private void Start()
        {
            joinButton.onClick.AddListener(JoinRelay);
            hostButton.onClick.AddListener(HostRelay);
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            joinCodeText.gameObject.SetActive(false);
            waitingForSecondPlayerText.gameObject.SetActive(false);
        }

        private void OnClientConnected(ulong _clientID)
        {
            // if (NetworkManager.Singleton.IsHost)
            //     return;
            if(_clientID == 1)
                gameObject.SetActive(false);
        }

        private void JoinRelay()
        {
            if (joinCodeInputField.text.Length == 0)
                return;
            RelayTest.Instance.JoinRelay(joinCodeInputField.text);
        }

        private void HostRelay()
        {
            RelayTest.Instance.CreateRelay();
            joinCodeText.gameObject.SetActive(true);
            joinCodeText.text = "Creating...";
            StartCoroutine(WaitForRelayJoinCode());
        }

        private IEnumerator WaitForRelayJoinCode()
        {
            yield return new WaitUntil(() => RelayTest.Instance.JoinCode != null);
            joinCodeText.text = RelayTest.Instance.JoinCode;
            joinCodeText.text = $"Join code: {RelayTest.Instance.JoinCode}";
            waitingForSecondPlayerText.gameObject.SetActive(true);
        }

    }
}
