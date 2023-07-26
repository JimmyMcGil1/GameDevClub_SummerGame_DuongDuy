using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrapScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            GameObject rock = transform.parent.Find("Rock").gameObject;
            rock.GetComponent<RollingRock>().StartRolling();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
