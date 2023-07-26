using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            collision.gameObject.GetComponent<BourbonMoveset>().TakeDamage(-damage);
        }
        GetComponent<Animator>().SetTrigger("hit");
    }
    public void Explode()
    {
        Destroy(gameObject);
    }
}
