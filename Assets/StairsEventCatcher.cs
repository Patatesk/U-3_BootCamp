using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;

namespace SametJR
{
    public class StairsEventCatcher : EventCatchBase
    {
        private List<GameObject> childs = new();
        private bool isAnimating = false;
        private Direction direction = Direction.Down;
        private int index = 0;
        private void Start() {
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i).gameObject);
            }
        }

        // [ContextMenu("Start Event")]
        // private void Deneme()
        // {
        //     index = 0;
        //     PerformStairAnimations(childs[0]);
        // }

        // [ContextMenu("End Event")]
        // private void Deneme2()
        // {
        //     index = 0;
        //     PerformStairAnimations(childs[0], Direction.Down);
        // }
        protected override void PerformStartEvent(int _channel)
        {
            if(isAnimating) return;
            isAnimating = true;
            index = 0;
            PerformStairAnimations(childs[0], Direction.Up);
        }

        protected override void PerformEndEvent(int _channel)
        {
            if(isAnimating) return;
            isAnimating = true;
            index = 0;
            PerformStairAnimations(childs[0], Direction.Down);
        }

        private void PerformStairAnimations(GameObject stair, Direction direction)
        {
            if(direction == this.direction) return;

            

            LeanTween.moveY(stair, stair.transform.position.y + (direction == Direction.Up ? 8f : -8f), .5f).setEaseOutBack().setOnComplete(() => {
                if(index >= childs.Count - 1)
                {
                    // isAnimating = false;
                    this.direction = direction;
                    return;
                } 
                PerformStairAnimations(childs[++index], direction);
            }).setOnComplete(
                () => isAnimating = false
            );
        }

        enum Direction
        {
            Up,
            Down
        }




    }
}
