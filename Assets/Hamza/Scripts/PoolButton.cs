using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BootCamp.Hamza
{
    public class PoolButton : MonoBehaviour
    {
        public PoolPlatform platform1;
        public PoolPlatform platform2;

        [ContextMenu("Move")]
        public void MovePlatforms()
        {
            if (platform1.canMove && platform2.canMove)
            {
                platform1.MovePlatform();
                platform2.MovePlatform();
            }
            else
            {
                Debug.Log("One of the platforms has something in its way.");
            }
        }
    }
}
