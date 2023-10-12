using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Pet
{
    public abstract class PetState
    {
       
            protected PetController controller;
            public virtual void OnEnter(PetController ctrler)
            {
                controller = ctrler;
            }
            public virtual void UpdateState() { }
            public virtual void OnHurt() { }
            public virtual void OnExit() { }
    }
}