using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

namespace BootCamp.SametJR
{

    public class NetworkInitializer : MonoBehaviour
    {
        [Header("Untick it if you want to attach custom Transform points to spawn players")]
        public bool useDefaultSpawnPoints = true;
        
        [DrawIf("useDefaultSpawnPoints", false)]public Transform spawnPoint1, spawnPoint2;
        private void Start()
        {
            GameObject go = new GameObject("NetworkManager");
            NetworkManager nm = go.AddComponent<NetworkManager>();
            UnityTransport ut = go.AddComponent<UnityTransport>();

            nm.NetworkConfig.NetworkTransport = ut;

            GameObject go2 = new GameObject("NetworkVariables");
            NetworkPlayerSpawner nps = go2.AddComponent<NetworkPlayerSpawner>();
            nps.bigPlayerPrefab = Resources.Load<GameObject>("BigPlayer");
            nps.smallPlayerPrefab = Resources.Load<GameObject>("SmallPlayer");
            if(useDefaultSpawnPoints)
            {
                nps.spawnPoint1 = Vector3.left;
                nps.spawnPoint2 = Vector3.right;
            }
            else
            {
                nps.spawnPoint1 = spawnPoint1.position;
                nps.spawnPoint2 = spawnPoint2.position;
            }


        }
    }
}
