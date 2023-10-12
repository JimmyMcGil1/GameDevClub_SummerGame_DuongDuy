using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Units.Pet
{
    public class PetIdleState : PetState
    {
        public override void OnEnter(PetController ctrler)
        {
            base.OnEnter(ctrler);
        }
        public override void UpdateState()
        {
            controller.CheckFlip();
            controller.ChangeState(controller.attackState);
        }
        
    }
}