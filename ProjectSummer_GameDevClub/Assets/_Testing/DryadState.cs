using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Testing
{
    public class DryadState : StateScene
{
    [SerializeField] GameObject dryadPref;
    [SerializeField] GameObject rockPref;

        public override void OnEnterState(ControlStateInScene ctrler)
        {
            base.OnEnterState(ctrler);
            GameObject dryad = Instantiate(dryadPref, transform.parent.Find("Dryad Init").position, Quaternion.identity);
            GameObject rock = Instantiate(rockPref, transform.parent.Find("Rock init").position, Quaternion.identity);
        }
        public override void ExitState()
        {
            base.ExitState();
            controller.ChangeState(transform.parent.Find("DryadState").GetComponent<StateScene>());
        }
    }
}

