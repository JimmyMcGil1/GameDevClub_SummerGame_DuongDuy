using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Units.Enemy
{
    public class Jaw : EnemyTier3Script
    {
        [SerializeField] public bool isHor = false;

        // Update is called once per frame
        protected override void Update()
        {

            MoveInAxis();
            
        }
        void MoveInAxis()
        {
            if (counterInOneDirect > timeInOneDirect)
            {
                TurnAround();
            }
            counterInOneDirect += Time.deltaTime;
            MoveInDirection(dirMove);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
            {
                TurnAround();
            }
        }
        protected new void TurnAround()
        {
            if (isHor)
                dirMove.x *= -1;
            else dirMove.y *= -1;
            counterInOneDirect = 0;
        }
    }
}