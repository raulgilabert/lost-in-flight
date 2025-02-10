using System;
using FSM;
using UnityEngine;
using StateMachineBehaviour = FSM.StateMachineBehaviour;

namespace GlobalState.FSM
{
    [Serializable]
    public class TransitionOnPlayerDeath : StateMachineBehaviour
    {
        [SerializeField] private string targetState;

        private Health.Health _playerHealth;
        private uint _targetStateIndex;

        public void Start(StateMachine stateMachine)
        {
            _playerHealth = GameManager.Instance.player.GetComponent<Health.Health>();
            _targetStateIndex = stateMachine.GetStateIndex(targetState);
        }

        public bool ShouldTransition(StateMachine stateMachine, out uint nextState)
        {
            nextState = _targetStateIndex;
            return _playerHealth.IsDead;
        }

        public string GetLabel()
        {
            return $"Transition to [{targetState}] on player death";
        }
    }
}