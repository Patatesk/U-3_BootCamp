using BootCamp.PK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SametJR
{
    public class LaserCoverEventCarher : EventCatchBase
    {
        [SerializeField] private GameObject[] lasers;

        [SerializeField] private int laserOneChannel, laserTwoChannel,laserThreeChannel;

        protected override void PerformStartEvent(int _channel)
        {
            if (_channel == laserOneChannel) 
            {
                if (lasers[0].activeSelf) lasers[0].gameObject.SetActive(false);
                else
                {
                    lasers[0].gameObject.SetActive(true);
                }
            }

            if (_channel == laserTwoChannel)
            {
                if (lasers[1].activeSelf) lasers[1].gameObject.SetActive(false);
                else
                {
                    lasers[1].gameObject.SetActive(true);
                }
            }

            if (_channel == laserThreeChannel)
            {
                if (lasers[2].activeSelf) lasers[2].gameObject.SetActive(false);
                else
                {
                    lasers[2].gameObject.SetActive(true);
                }
            }
        }

    }
}
