using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDamage : MonoBehaviour
{
    public Rigidbody2D rigit;
    public float minVeloc = 2f;
    public float damageMultiplier = 5;
    public UnityFloatEvent onDamageTaken;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float veloc = collision.relativeVelocity.magnitude;
        if (veloc > minVeloc)
        {
            onDamageTaken.Invoke(veloc * damageMultiplier);
        }
    }
}

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float>
{

}