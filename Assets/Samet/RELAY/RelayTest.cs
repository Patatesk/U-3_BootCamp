using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System;

namespace SametJR
{
    public class RelayTest : MonoBehaviour
    {
        #region Singleton
        public static RelayTest Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }
        #endregion
        public string JoinCode { get; private set; }
        private async void Start()
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log($"Signed in as {AuthenticationService.Instance.PlayerId}");
            };

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            // CreateRelay();
        }

        public async void CreateRelay()
        {
            try
            {

                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log($"Join code: {joinCode}");

                RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

                NetworkManager.Singleton.StartHost();
                JoinCode = joinCode;
            }
            catch (RelayServiceException e)
            {
                Debug.Log(e.Message);
            }
        }

        public async void JoinRelay(string joinCode)
        {
            try
            {
                Debug.Log($"Joining relay with code {joinCode}");
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

                RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

                NetworkManager.Singleton.StartClient();

                CloseCanvasServerRpc();
            } catch (RelayServiceException e) {

                Debug.Log(e.Message);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void CloseCanvasServerRpc()
        {
            RelayUI.Instance.CloseCanvasClientRpc();
        }
    }
}
