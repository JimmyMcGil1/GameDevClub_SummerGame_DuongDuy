using System.Collections;
using System.Collections.Generic;
using Units.Bourbon;
using UnityEngine;

namespace Units.Borubon
{
    public class DashState : State
    {

        public override void OnEnter(BourbonController _ctrler)
        {
            base.OnEnter(_ctrler);
            controller.moveSet.HandleDashInput();
        }
        public override void UpdateState()
        {
            controller.ChangeState(controller.idleState);
        }
    }
}