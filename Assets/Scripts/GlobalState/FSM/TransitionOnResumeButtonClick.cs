using System;
using FSM;
using UnityEngine;
using UnityEngine.UI;
using StateMachineBehaviour = FSM.StateMachineBehaviour;

namespace GlobalState.FSM
{
    [Serializable]
    public class TransitionOnResumeButtonClick : StateMachineBehaviour
    {
        [SerializeField] private string targetState;
        
        private Button _button;

        private bool _shouldTransition;
        private uint _targetStateIndex;

        public void Start(StateMachine stateMachine)
        {
            _targetStateIndex = stateMachine.GetStateIndex(targetState);
            _button = GameManager.Instance.resumeButton;
        }

        public void OnStateEnter(StateMachine stateMachine)
        {
            _shouldTransition = false;
            _button.onClick.AddListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            _shouldTransition = true;
        }

        public void OnStateExit(StateMachine stateMachine)
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public bool ShouldTransition(StateMachine stateMachine, out uint nextState)
        {
            nextState = _targetStateIndex;
            return _shouldTransition;
        }

        public string GetLabel()
        {
            return $"Transition to [{targetState}] on resume button click";
        }
    }
}