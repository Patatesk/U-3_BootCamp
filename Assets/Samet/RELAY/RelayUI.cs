using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SametJR
{
    public class RelayUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField joinCodeInputField;
        [SerializeField] private Button joinButton;
        [SerializeField] private Button hostButton;
        [SerializeField] private TextMeshProUGUI joinCodeText;

        private void Start()
        {
            joinButton.onClick.AddListener(JoinRelay);
            hostButton.onClick.AddListener(HostRelay);
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
            StartCoroutine(WaitForRelayJoinCode());
        }

        private IEnumerator WaitForRelayJoinCode()
        {
            yield return new WaitUntil(() => RelayTest.Instance.JoinCode != null);
            joinCodeText.text = RelayTest.Instance.JoinCode;
        }
    }
}
