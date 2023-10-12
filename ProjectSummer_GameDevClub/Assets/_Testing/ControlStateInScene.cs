using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class ControlStateInScene : MonoBehaviour
    {
        StateScene currentState;
        public void ChangeState(StateScene newState)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }
            newState.OnEnterState(this);
            currentState = newState;
        }
        private void Awake()
        {
            ChangeState(transform.Find("WolfState").GetComponent<StateScene>());
        }
        private void Update()
        {
            currentState?.UpdateState();
        }
    }
}
