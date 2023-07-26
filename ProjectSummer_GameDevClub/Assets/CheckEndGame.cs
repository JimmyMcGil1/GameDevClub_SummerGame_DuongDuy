using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckEndGame : MonoBehaviour
{
    [SerializeField] GameObject[] bricks;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            if (!Systems.isEndGame)
            {
                foreach (var b in bricks)
                {
                    b.GetComponent<Animator>().SetTrigger("broke");
                    b.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                Debug.Log(Systems.isEndGame.ToString());
                Systems.instance.EndGame();
            }
        }
    }
    void ResetWhenPlayerDead()
    {
        foreach (var b in bricks)
        {
            b.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnEnable()
    {
        BourbonUnitBase.bourbonDead += ResetWhenPlayerDead;
    }
    private void OnDisable()
    {
        BourbonUnitBase.bourbonDead -= ResetWhenPlayerDead;
    }
}
