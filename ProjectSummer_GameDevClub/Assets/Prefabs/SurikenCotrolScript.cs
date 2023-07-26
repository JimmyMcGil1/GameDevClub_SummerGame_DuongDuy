using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurikenCotrolScript : MonoBehaviour
{
    [SerializeField] protected float timeInOneDirect = 3;
    protected float counterInOneDirect = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        DiChuyenTheoNanQuat();
    }
    void DiChuyenTheoNanQuat()
    {
        HingeJoint2D hj = GetComponent<HingeJoint2D>();
        JointMotor2D motor = hj.motor;
        if (counterInOneDirect > timeInOneDirect)
        {
            motor.motorSpeed = -motor.motorSpeed;
            counterInOneDirect = 0;
        }
        hj.motor = motor;
        counterInOneDirect += Time.fixedDeltaTime;
    }
}
