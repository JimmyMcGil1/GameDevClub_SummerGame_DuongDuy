using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadKick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bourbon")) {
            Debug.Log("Hit character");
            Rigidbody2D rigit = other.GetComponent<Rigidbody2D>();
          //  rigit.AddForce(Vector2.left  * 3 * rigit.mass, ForceMode2D.Impulse);
            StartCoroutine("Kick", rigit);
        }
    }   
    IEnumerator Kick(Rigidbody2D rigit) {
            rigit.velocity = Vector2.left * rigit.mass * 10f;
            yield return new WaitForSeconds(0.5f);
            rigit.velocity = new Vector2(0, rigit.velocity.y);
    }
}
