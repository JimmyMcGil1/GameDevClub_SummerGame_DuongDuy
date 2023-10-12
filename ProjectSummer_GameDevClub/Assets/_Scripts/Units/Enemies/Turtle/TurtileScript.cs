using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Enemy
{
    public class TurtileScript : EnemyTier3Script
    {

        [SerializeField] float floatSpeed;
        [SerializeField] float amplitude;
        float y0;
        private Vector3 tempPos;

        private void Start()
        {
            y0 = transform.position.y;
            tempPos = transform.position;
            healSld = transform.Find("Enemy_t1_2_Canvas").Find("healSld").gameObject.GetComponent<Slider>();
            healSld.maxValue = _Stats.Health;
            healSld.value = _Stats.Health;
            healSld.gameObject.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            if (counterInOneDirect > timeInOneDirect)
            {
                dirMove.x *= -1;
                counterInOneDirect = 0;
            }
            counterInOneDirect += Time.deltaTime;
            TurtleMove();
        }
        void TurtleMove()
        {
            tempPos.y = y0 + amplitude * Mathf.Sin(floatSpeed * Time.time);
            tempPos.x += speed * Time.deltaTime * dirMove.x;
            transform.position = tempPos;

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bourbon"))
            {
                anim.SetTrigger("spikeOut");
            }
        }

    }
}