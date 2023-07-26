using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BourbonInWater : MonoBehaviour
{
     GameObject water;
    [SerializeField] float speedDive = 2;
    float oldDens;
    BuoyancyEffector2D buo;
    
    private void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        buo = water.GetComponent<BuoyancyEffector2D>();
        oldDens = buo.density;
    }
    // Update is called once per frame
    void Update()
    {
         if (BourbonUnitBase.inWater) MovementInWater();
    }
    void MovementInWater()
    {

        if (BourbonUnitBase._canMove)
        {
            if (Input.GetKey(KeyCode.S))
            {
                buo.density -= speedDive * Time.deltaTime;
            }
            else if (buo.density < oldDens)
            {
                buo.density += speedDive * Time.deltaTime;
            }
        }
    }
}
