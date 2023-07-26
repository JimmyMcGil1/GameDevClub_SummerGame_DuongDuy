using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Heal : MonoBehaviour
{
    float heal;
    public static event Action playerDead;
    public static event Action<float> onPlayerHurt;
    UnityEvent getHeal;
    private void Start()
    {
        playerDead = null;
    }
    public void TakeDame(float dmg)
    {
        heal -= dmg;
        if (heal < 0 )
        {
            playerDead?.Invoke();
            getHeal.Invoke();
        }
    }
    public void ActionWhenPlayerDead()
    {

    }
}
