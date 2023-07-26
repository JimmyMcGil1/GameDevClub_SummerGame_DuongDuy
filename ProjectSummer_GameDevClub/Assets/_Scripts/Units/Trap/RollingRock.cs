using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public void StartRolling()
    {
        Rigidbody2D rigit = GetComponent<Rigidbody2D>();
        rigit.AddForce(Vector2.left * 350, ForceMode2D.Impulse);
    }
    private void OnEnable()
    {
        BourbonUnitBase.bourbonDead += ResetPos;
    }
    private void OnDisable()
    {
        BourbonUnitBase.bourbonDead -= ResetPos;
    }
    public void ResetPos()
    {
        Rigidbody2D rigit = GetComponent<Rigidbody2D>();
        rigit.AddForce(Vector2.left * -350, ForceMode2D.Impulse);
        rigit.velocity = Vector2.zero;
        rigit.rotation = 0f;
        rigit.angularVelocity = 0f;
        Transform initPos = transform.parent.Find("InitPos");
        transform.localPosition = initPos.localPosition;
        GameObject rockTrap = transform.parent.Find("RockTrap").gameObject;
        rockTrap.GetComponent<BoxCollider2D>().enabled = true;
    }
}
