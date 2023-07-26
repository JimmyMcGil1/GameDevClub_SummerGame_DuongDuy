using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankColliderScript : MonoBehaviour
{
    [SerializeField] float forceDown;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
           // GetComponent<Rigidbody2D>().AddForce(Vector2.up * forcedown, ForceMode2D.Impulse);
            Vector2.MoveTowards(transform.position, Vector2.down * 2, forceDown * Time.deltaTime );
        }
    }
}
