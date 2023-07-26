using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jaw : EnemyTier3Script
{
    

    // Update is called once per frame
    void Update()
    {
      
            MoveInAxis();
       
    }
    void MoveInAxis()
    {
        if (counterInOneDirect > timeInOneDirect)
        {
            if (isHor)
                dirMove.x *= -1;
            else dirMove.y *= -1;
            counterInOneDirect = 0;
        }
        counterInOneDirect += Time.deltaTime;
        MoveInDirection(dirMove);
    }
    
}
