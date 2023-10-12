using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public void FalledRock()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //tesitng 
        Destroy(gameObject);
    }
    public void ResetRock()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            GetComponent<Animator>().SetBool("isBroken", true);
            
        }
    }
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Bourbon"))
    //     {
    //         GetComponent<Animator>().SetBool("isBroken", false);
    //     }
    // }
}
