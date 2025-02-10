using System;
using FSM;
using UnityEngine;
using StateMachineBehaviour = FSM.StateMachineBehaviour;

namespace GlobalState.FSM
{
    [Serializable]
    public class HelloWorldState : StateMachineBehaviour
    {
        [SerializeField] private string helloName;

        public void OnStateEnter(StateMachine stateMachine)
        {
            Debug.Log($"Hello {helloName}");
        }
    }
}