using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Bourbon
{
    public class MoveState : State
    {
        public override void OnEnter(BourbonController _ctrler)
        {
            base.OnEnter(_ctrler);
            controller.moveStates = CharacterMoveStates.Walking;
          // old anim code controller.moveSet.anim.SetBool("walk", true);
            controller.moveSet.sktAnim.state.AddAnimation(0, controller.moveSet.runAnimationName,true, -0.6f);
        }
        public override void UpdateState()
        {
            if ((controller.HitWallLeft() || controller.HitWallRight()) && !controller.IsGround() 
            && (controller.moveSet.rigit.velocity.y < 0) && controller.moveSet.PlayerWantClingeOnWall())
            {
                controller.ChangeState(controller.cringeOnWallState);
                return;
            }
            controller.moveSet.HandleMovementInput();
            if (!controller.IsGround())
            {
                controller.moveSet.Falling();
            }
            if (controller.moveSet.horizotal == 0)
            {
                this.controller.ChangeState(this.controller.idleState);
                return;
            }
            if (controller.moveSet.jumpInput)
                controller.ChangeState(this.controller.jumpState);
            else if (controller.moveSet.dashInput)
            {
                controller.ChangeState(controller.dashState);
            }

        }
        public override void OnExit()
        {
            base.OnExit();
           // old code anim controller.moveSet.anim.SetBool("walk", false);
            controller.moveSet.sktAnim.state.AddAnimation(0, controller.moveSet.idleAnimationName,true, -0.6f);
        }
    }

}