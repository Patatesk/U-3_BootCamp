using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BootCamp.PK;
using Unity.VisualScripting;

namespace SametJR
{
    public class StairsEventCatcher : EventCatchBase
    {
        [SerializeField] private int setChannel;
        private List<GameObject> childs = new();
        private bool isAnimating = false;
        private Direction direction = Direction.Down;
        private Direction lastDirection = Direction.Down;
        private void Start() {
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i).gameObject);
            }
        }

        
        protected override void PerformStartEvent(int _channel)
        {
            if (_channel == setChannel)
            {
                isAnimating = true;
                return;
            }
            lastDirection = Direction.Up;
            if(isAnimating) return;           
            if(direction != Direction.Up)
               StartCoroutine(PerformStairAnimations(childs[0], Direction.Up));
        }

        protected override void PerformEndEvent(int _channel)
        {
            lastDirection = Direction.Down;
            if (isAnimating) return;           
            if (direction != Direction.Down)
                StartCoroutine(PerformStairAnimations(childs[0], Direction.Down));
        }

        

        private IEnumerator PerformStairAnimations(GameObject stair, Direction direction)
        {
            isAnimating = true;
            foreach (var item in childs)
            {
                LeanTween.moveY(item, item.transform.position.y + (direction == Direction.Up ? 8f : -8f), .5f).setEaseOutBack();
                yield return new WaitForSeconds(.5f);
            }
            isAnimating = false;
            this.direction = direction;
            if(lastDirection != direction) StartCoroutine(PerformStairAnimations(childs[0], lastDirection));
        }

        enum Direction
        {
            Up,
            Down
        }
       




    }
}
