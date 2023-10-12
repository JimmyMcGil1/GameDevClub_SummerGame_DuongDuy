using System.Collections;
using System.Collections.Generic;
using Units.Bourbon;
using UnityEngine;

namespace Units.Trap
{
    public class CoconutObject : MonoBehaviour
    {
        [SerializeField] float speedFall;
        [SerializeField] int damage;
        public bool isFalling = false;
        private void Update()
        {
            if (isFalling)
                MoveDown();
        }
        void MoveDown()
        {
            Vector2 currentPos = transform.position;
            currentPos.y -= Time.deltaTime * speedFall;
            transform.position = currentPos;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bourbon"))
            {
                other.gameObject.GetComponent<BourbonController>().TakeDamage(-damage);
                Invoke("Destroy", 0.3f);
            }
            if (other.gameObject.layer == 6)
                Destroy();

        }
        void Destroy()
        {
            Destroy(gameObject);
        }
    }

}
