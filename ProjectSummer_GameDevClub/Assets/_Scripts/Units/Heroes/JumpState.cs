using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Bourbon
{
    public class JumpState : State
    {
        public override void OnEnter(BourbonController _ctrler)
        {
            base.OnEnter(_ctrler);
            //set lai trong luc
        }
        public override void UpdateState()
        {
            
            this.controller.moveSet.HandleJumpInput();
            if ((controller.HitWallLeft() || controller.HitWallRight()) && !controller.IsGround() 
            && controller.moveSet.rigit.velocity.y < 0)
            {
                controller.ChangeState(controller.cringeOnWallState);
            }
            else controller.ChangeState(controller.idleState);
            //sau khi dat 1 do cao cu the thi chuyen sang fall
            //if (controller.moveSet.dashInput)
            //{
            //    controller.ChangeState(controller.dashState);
            //    return;
            //}
            //else if (!controller.isGround && controller.isHitWall)
            //{
            //    this.controller.ChangeState(this.controller.cringeOnWallState);
            //}
            //else
            //     controller.ChangeState(controller.idleState);

        }
        public override void OnExit()
        {

        }
    }
}