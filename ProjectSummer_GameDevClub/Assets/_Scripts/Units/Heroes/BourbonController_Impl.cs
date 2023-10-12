using System.Collections;
using System.Collections.Generic;
using Units.Borubon;
using UnityEngine;
namespace Units.Bourbon
{
    public partial class BourbonController
    {

        public State currentState;
        
        public IdleState idleState = new IdleState();
        public MoveState walkState = new MoveState();
        public JumpState jumpState = new JumpState();
        public DashState dashState = new DashState();
        public FallState fallState = new FallState();
        public CringeOnWallState cringeOnWallState = new CringeOnWallState();

        public void ChangeState(State newState)
        {
            if (currentState != null )
            {
                currentState.OnExit();
            }
            newState.OnEnter(this);
            currentState = newState;
        }

        public void UpdateSM()
        {
            currentState.UpdateState();
        }

        //void EnterSM(CharacterMoveStates newState)
        //{
        //    ///TODO
        //    switch (newState)
        //    {
        //        case CharacterMoveStates.Idle:
        //            OnEnterIdle();
        //            break;
        //        }
        //    }


        //void ExitSM(CharacterMoveStates oldState)
        //{
        //    ///TODO
        //    switch (oldState)
        //    {
        //        case CharacterMoveStates.Idle:
        //            OnExitIdle();
        //            break;
        //    }
        //}


        //void OnEnterIdle()
        //{

        //}

        //CharacterMoveStates OnUpdateIdle()
        //{
        //    return CharacterMoveStates.Idle;
        //}

        //void OnExitIdle()
        //{

        //}
    }
    
}
