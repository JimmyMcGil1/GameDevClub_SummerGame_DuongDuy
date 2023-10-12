using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Bourbon
{
    public class FallState : State
    {
        public override void OnEnter(BourbonController _ctrler)
        {
            base.OnEnter(_ctrler);
            //set trong luc 

        }
        public override void UpdateState()
        {
            //neu tiep dat thi -> idle state
            controller.moveSet.Falling();
            if (this.controller.moveSet.horizotal != 0 && controller._canMove && controller.condition == CharacterConditions.Normal && this.controller.moveSet.timeCounter == 0)
            {
                this.controller.ChangeState(this.controller.walkState);
            }
            else if (this.controller.moveSet.jumpInput)
            {
                this.controller.ChangeState(this.controller.jumpState);
            }
            else if (controller.IsGround()) controller.ChangeState(controller.idleState);
        }
        public override void OnExit()
        {
            controller.moveSet.TiepDat();
        }
    }
}
