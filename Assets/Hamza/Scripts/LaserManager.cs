using UnityEditor;
using UnityEngine;

namespace BootCamp.Hamza
{
    public class LaserManager : MonoBehaviour
    {
        [Tooltip("Number of lasers")]
        public int laserCount = 5;
        [Tooltip("Offset between lasers")]
        public float laserOffset = 2f;
        [Tooltip("Maximum length of the lasers")]
        public float maxLength = 100f;
        [Tooltip("Prefab for laser game object")]
        public GameObject laserPrefab;
        [Tooltip("Layers to detect collisions with")]
        public LayerMask hitLayers;
        [Tooltip("Tag for the player object")]
        public string playerTag = "Player";

        private LineRenderer[] lineRenderers;

        private void Start()
        {
            StartDraw();
        }
        [ContextMenu("Draw")]
        private void StartDraw()
        {
            //int childCount = transform.childCount;
            //for (int i = 0; i < childCount; i++)
            //{
            //   DestroyImmediate(transform.GetChild(0).gameObject);
            //}
            lineRenderers = new LineRenderer[laserCount];

            // Calculate the total offset
            float totalOffset = (laserCount - 1) * laserOffset;

            // Create lasers and set up LineRenderers
            for (int i = 0; i < laserCount; i++)
            {
                // Calculate the offset for current laser
                float offset = i * laserOffset - totalOffset / 2f;

                Ray ray = new Ray(transform.position + new Vector3(offset, 0f, 0f), transform.forward);

                // Instantiate a laser game object
                GameObject laserObject = Instantiate(laserPrefab, transform);
                laserObject.transform.localPosition = new Vector3(offset, 0f, 0f);
                laserObject.transform.localRotation = Quaternion.identity;

                // Get the LineRenderer component from the instantiated game object
                LineRenderer lineRenderer = laserObject.GetComponent<LineRenderer>();
                lineRenderers[i] = lineRenderer;

                // Set the initial positions of the LineRenderer
                lineRenderer.SetPosition(0, Vector3.zero);
                lineRenderer.SetPosition(1, Vector3.forward * maxLength);
            }
        }

        private void Update()
        {
            for (int i = 0; i < laserCount; i++)
            {
                // Calculate the offset for current laser
                float offset = i * laserOffset - ((laserCount - 1) * laserOffset) / 2f;

                Ray ray = new Ray(lineRenderers[i].transform.position, transform.forward);
                RaycastHit hit;

                // Cast the ray and update LineRenderer positions
                if (Physics.Raycast(ray, out hit, maxLength, hitLayers))
                {
                    lineRenderers[i].SetPosition(1, new Vector3(0f, 0f, hit.distance));

                    // Check if the ray hits the player
                    if (hit.collider.CompareTag(playerTag))
                    {
                        Debug.Log("Player hit by laser!");
                        Debug.Log("Hit" + i);

                    }
                }
                else
                {
                    lineRenderers[i].SetPosition(1, Vector3.forward * maxLength);
                }
            }
        }
    }
}
