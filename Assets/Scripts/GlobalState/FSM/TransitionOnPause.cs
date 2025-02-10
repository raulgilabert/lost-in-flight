using System;
using FSM;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachineBehaviour = FSM.StateMachineBehaviour;

namespace GlobalState.FSM
{
    [Serializable]
    public class TransitionOnPause : StateMachineBehaviour
    {
        [SerializeField] private string targetState;
        
        private InputAction _pauseAction;
        private uint _targetStateIndex;

        public void Start(StateMachine stateMachine)
        {
            _pauseAction = InputSystem.actions.FindAction("Pause");
            _targetStateIndex = stateMachine.GetStateIndex(targetState);
        }

        public bool ShouldTransition(StateMachine stateMachine, out uint nextState)
        {
            nextState = _targetStateIndex;
            return _pauseAction.WasPerformedThisFrame();
        }

        public string GetLabel()
        {
            return $"Transition to [{targetState}] on pause";
        }
    }
}