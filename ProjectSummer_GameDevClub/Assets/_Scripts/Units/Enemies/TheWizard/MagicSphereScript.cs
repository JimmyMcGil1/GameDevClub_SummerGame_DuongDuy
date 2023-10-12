using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Bourbon;
public class MagicSphereScript : MonoBehaviour
{
    public int damage;
    
    public void Disapear()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            collision.gameObject.GetComponent<BourbonController>().TakeDamage(-damage);
        }
    }
}
