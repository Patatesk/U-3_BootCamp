using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace BootCamp.SametJR
{
    public class NetworkPlayerSpawner : NetworkBehaviour
    {
        public NetworkVariable<int> playerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public GameObject bigPlayerPrefab, smallPlayerPrefab;
        [DrawIf("useSpawnPoints", true)] public Transform spawnPoint1, spawnPoint2;
        public bool useSpawnPoints = false;
        
        

        private void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        }

        private void OnDisable()
        {
            if (NetworkManager.Singleton == null) return;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
        }

        private void OnClientConnected(ulong clientId)
        {
            // we should not run this code twice (once for the server and once for the client)
            if (!NetworkManager.Singleton.IsServer) return;
            Debug.Log("Client connected" + clientId + " ---- " + playerCount.Value);
            playerCount.Value++;
            CheckCameraInitilizationServerRpc();
            SpawnPlayerServerRpc(clientId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void CheckCameraInitilizationServerRpc()
        {
            if (playerCount.Value == 2)
            {
                SetCameraPlayerTransformsClientRPC();
            }
        }


        [ClientRpc]
        private void SetCameraPlayerTransformsClientRPC()
        {
            StartCoroutine(TestCoroutine());
        }

        private IEnumerator TestCoroutine()
        {
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Player").Length == 2);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Transform a = players[0].transform;
            Transform b = players[1].transform;
            CameraFollow.Instance.InitPlayertransforms(a, b);
        }



        private void OnClientDisconnect(ulong clientId)
        {
            playerCount.Value--;
        }



        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayerServerRpc(ulong clientId)
        {
            if (playerCount.Value == 1)
            {
                GameObject player = Instantiate(bigPlayerPrefab, useSpawnPoints ? spawnPoint1.position : Vector3.left, Quaternion.identity);
                //player.GetComponent<CharacterMovement>().SetStartPosition(useSpawnPoints ? spawnPoint1.position : Vector3.left);
                player.tag = "Player";
                player.name = "BigPlayer";
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
                player.SetActive(false);
                // player.transform.position = useSpawnPoints ? spawnPoint1.position : Vector3.left;
                player.SetActive(true);
            }
            else if (playerCount.Value == 2)
            {
                GameObject player = Instantiate(smallPlayerPrefab, useSpawnPoints ? spawnPoint2.position : Vector3.right, Quaternion.identity);
                //player.GetComponent<CharacterMovement>().SetStartPosition(useSpawnPoints ? spawnPoint1.position : Vector3.left);
                player.tag = "Player";
                player.name = "SmallPlayer";
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
                player.SetActive(false);
                // player.transform.position = useSpawnPoints ? spawnPoint2.position : Vector3.right;
                player.SetActive(true);
            }
        }


        

    }
}
