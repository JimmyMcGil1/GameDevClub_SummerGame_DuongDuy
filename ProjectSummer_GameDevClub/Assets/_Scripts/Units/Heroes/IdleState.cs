using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Bourbon
{
    public class IdleState : State
    {
        public override void OnEnter(BourbonController ctrler)
        {
            base.OnEnter(ctrler);
        }
        public override void UpdateState()
        {
            if ((controller.HitWallLeft() || controller.HitWallRight()) && !controller.IsGround()
            && (controller.moveSet.rigit.velocity.y < 0) && controller.moveSet.PlayerWantClingeOnWall())
            {
                controller.ChangeState(controller.cringeOnWallState);
                return;
            }
            if (!controller.IsGround())
            {
                controller.moveSet.Falling();
            }
            else
            {
                controller.moveSet.rigit.gravityScale = 3f;
                controller.moveSet.jumpCounter = 0;

            }
            if (this.controller.moveSet.horizotal != 0 && controller._canMove && controller.condition == CharacterConditions.Normal)
            {
                this.controller.ChangeState(this.controller.walkState);
            }

            if (this.controller.moveSet.jumpInput)
            {
                this.controller.ChangeState(this.controller.jumpState);
            }
            else if (this.controller.moveSet.dashInput)
            {
                this.controller.ChangeState(this.controller.dashState);
            }

        }
       
    }
}
