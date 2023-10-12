using System.Collections;
using System.Collections.Generic;
using Units.Bourbon;
using UnityEngine;

public class BourbonInWater : MonoBehaviour
{
     GameObject water;
    [SerializeField] float speedDive = 2;
    float oldDens;
    BuoyancyEffector2D buo;
    BourbonController controller;

    private void Awake()
    {
        controller = GetComponent<BourbonController>();
    }
    private void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        if (water != null)
        {
            buo = water.GetComponent<BuoyancyEffector2D>();
            oldDens = buo.density;
        }
            
    }
    // Update is called once per frame
    void Update()
    {
         if (controller.condition == CharacterConditions.Water) MovementInWater();
    }
    void MovementInWater()
    {

        if (controller._canMove)
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
