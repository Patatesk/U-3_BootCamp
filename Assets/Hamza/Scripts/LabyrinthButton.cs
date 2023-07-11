using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.Hamza
{
    public class LabyrinthButton : MonoBehaviour
    {
        public GameObject wallToOpen;

        [ContextMenu("Open Wall")]
        public void OpenWall()
        {
            LeanTween.moveLocalY(wallToOpen, -1f, 2f).setEaseInOutQuad();
        }
    }
}
