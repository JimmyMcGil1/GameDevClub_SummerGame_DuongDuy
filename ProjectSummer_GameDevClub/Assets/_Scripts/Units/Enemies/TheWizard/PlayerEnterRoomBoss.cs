using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemy;
public class PlayerEnterRoomBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            transform.parent.Find("Enemy_t1_2_Canvas").gameObject.SetActive(true);
            transform.parent.GetComponent<SummonUnderlingScript>().enabled = true;
            transform.parent.GetComponent<WizardScrpt>().enabled = true;
        }
    }
}
