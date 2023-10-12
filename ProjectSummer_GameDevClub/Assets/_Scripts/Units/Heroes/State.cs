using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Bourbon {
    public abstract class State
    {
        protected BourbonController controller;
        public virtual void OnEnter(BourbonController _ctrler) {
            controller = _ctrler;
        }
        public virtual void UpdateState() { }
        public virtual void OnHurt() { }
        public virtual void OnExit() { }
    }
}
