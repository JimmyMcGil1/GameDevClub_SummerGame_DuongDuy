using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class StateScene: MonoBehaviour
    {
        protected ControlStateInScene controller;
        public virtual void OnEnterState(ControlStateInScene ctrler)
        {
            controller = ctrler;
        }
        public virtual void UpdateState()
        {

        }
        public virtual void ExitState()
        {

        }
    }

} 