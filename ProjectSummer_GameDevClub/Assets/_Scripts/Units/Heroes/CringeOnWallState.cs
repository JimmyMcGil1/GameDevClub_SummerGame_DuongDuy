using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Units.Bourbon
{
    public class CringeOnWallState : State
    {

        Vector2 dirHit = Vector2.right;
        float oldGravity;
        public override void OnEnter(BourbonController _ctrler)
        {
            base.OnEnter(_ctrler);
            // controller.moveSet.ClingingOnWall();
            controller.moveSet.rigit.velocity = Vector2.zero;
            controller.moveSet.jumpCounter = 0;
            if (controller.HitWallLeft()) dirHit = Vector2.left;
            controller.moveSet.rigit.velocity = new Vector2(controller.moveSet.rigit.velocity.x, 0);
            oldGravity = controller.moveSet.rigit.gravityScale;
            controller.moveSet.rigit.gravityScale = 0f;
            controller.moveSet.sktAnim.state.AddAnimation(0, controller.moveSet.hangAnimationName, true, -0.6f);
        }
        float clingeTimmer = 0.1f;
        float clingeCounter = 0f;

        public override void UpdateState()
        {
         
            if (controller.HitWallLeft() && controller.moveSet.horizotal == -1
            || controller.HitWallRight() && controller.moveSet.horizotal == 1 )
            {
                if (controller.moveSet.jumpInput)
                {
                    controller.moveSet.WallJumpHandle();
                }
                return;
            }
        
            if (controller.moveSet.jumpInput)
            {
                controller.moveSet.WallJumpHandle();
            }
            controller.ChangeState(controller.idleState);
           

        }
        public override void OnExit()
        {
            clingeCounter = 0f;
            controller.moveSet.sktAnim.state.AddAnimation(0, controller.moveSet.idleAnimationName, true, -0.6f);
            if (!controller.IsGround())
            {
                controller.moveSet.rigit.gravityScale = oldGravity;
            }
        }
    }
}