using System;
using FSM;
using UnityEngine;
using StateMachineBehaviour = FSM.StateMachineBehaviour;

namespace GlobalState.FSM
{
    [Serializable]
    public class SetTimeScale : StateMachineBehaviour
    {
        [SerializeField] private float timeScale = 1;

        private float _previousTimeScale;

        public void OnStateEnter(StateMachine stateMachine)
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = timeScale;
        }

        public void OnStateExit(StateMachine stateMachine)
        {
            Time.timeScale = _previousTimeScale;
        }

        public string GetLabel()
        {
            return $"Set time scale to {timeScale}";
        }
    }
}