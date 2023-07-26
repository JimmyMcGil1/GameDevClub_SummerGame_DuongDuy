using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCanvas : MonoBehaviour
{
    void displayCanvas()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        Heal.playerDead += displayCanvas;
    }
    private void OnDisable()
    {
        Heal.playerDead -= displayCanvas;

    }
}
