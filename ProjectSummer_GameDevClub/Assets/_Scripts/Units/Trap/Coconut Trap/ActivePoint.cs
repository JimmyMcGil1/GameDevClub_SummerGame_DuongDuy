using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Trap
{
    public class ActivePoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bourbon"))
            {
                Debug.Log("Trap active");
                transform.parent.GetComponent<TrapObject>().Active();
            }
        }
        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.gameObject.CompareTag("Bourbon"))
        //     {
        //         Debug.Log("Trap active");
        //         transform.parent.GetComponent<TrapObject>().Active();
        //     }
        // }
    }
}


