using System.Collections;
using System.Collections.Generic;
using Units.Pet;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System;
using UnityEditor.Experimental.GraphView;
namespace Units.Pet
{
    public class FollowMasterState : PetState
    {

          float followTimmer = 4f;
        float followCounter = 0;
        public override void OnEnter(PetController ctrler)
        {
            base.OnEnter(ctrler);
            
        }
      
        public override void UpdateState()
        {
            followCounter += Time.deltaTime;
            if (followCounter > followTimmer)
            {
                controller.ChangeState(controller.idleState);
            }
            else {
                if (controller.petFigure.flyingPet) {
                    controller.moveSet.aiFinding.canMove = true;
                    controller.CheckFlip();
                }
                else  
                {
                    if (!controller.petFigure.HitWall())
                    {
                            Vector3 pos = controller.petFigure.bourbon.transform.Find("PetApperPos").position;
                            Vector3 dir = pos - controller.petFigure.transform.position;
                            //   Rigidbody2D rigit = controller.petFigure.GetComponent<Rigidbody2D>();
                            if (dir.magnitude > 1) 
                            {
                                controller.petFigure.transform.Translate(new Vector3(Mathf.Sign(dir.x) * 5 * Time.deltaTime, 0, 0));
                            }
                            //    rigit.velocity = new Vector2(2 * Mathf.Sign(dir.x), 0 );
                            //   else rigit.velocity = new Vector2(0, rigit.velocity.y);
                    }
                    else 
                    {
                            float dir = controller.petFigure.bourbon.transform.position.x - controller.petFigure.transform.position.x;
                            Vector3 targetPos = new Vector3(Mathf.Sign(dir) ,controller.petFigure.bourbon.transform.position.y + 0.5f);
                            controller.petFigure.transform.DOJump(targetPos, 0.8f, 1, 0.5f,false);
                    }
                }
               
            }
        }
        public override void OnExit()
        {
            followCounter = 0;
            if (controller.petFigure.flyingPet) {
                 controller.moveSet.aiFinding.canMove = false;
            }
        }
    } 
}
