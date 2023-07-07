using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class PlatformPlacer : MonoBehaviour
    {
        public List<GameObject> platforms = new();
        public float gap = 3f;
        private void Start() {
            // all platforms will be placed such a way that every platform will be the top right corner of the previous platform
            // consider placing gap between platforms
            for (int i = 0; i < platforms.Count; i++)
            {
                if (i == 0) continue;
                platforms[i].transform.position = platforms[i - 1].transform.position + new Vector3(platforms[i - 1].transform.localScale.x + gap, 0, platforms[i - 1].transform.localScale.z + gap);
            }
        }
    }
}
