using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateMachineGraph graph;
        
        public UnityEvent<string> onStateEnter;
        public UnityEvent<string> onStateExit;
        
        public string CurrentStateName => CurrentState.name;

        private uint _currentStateIndex;
        
        private StateMachineGraph.State CurrentState => graph.states[_currentStateIndex];

        private void Start()
        {
            _currentStateIndex = 0;

            foreach (var state in graph.states)
            {
                foreach (var behaviour in state.behaviours)
                {
                    behaviour.Start(this);
                }
            }
            
            RunOnEnter();
        }

        private void Update()
        {
            if (_currentStateIndex >= graph.states.Length)
            {
                Debug.LogError($"Current state index is out of range ({_currentStateIndex})");
                Debug.LogWarning("Disabling FSM due to deadlock");
                enabled = false;
            }

            RunOnUpdate();

            foreach (var behaviour in CurrentState.behaviours)
            {
                if (behaviour.ShouldTransition(this, out var nextState))
                {
                    RunOnExit();
                    _currentStateIndex = nextState;
                    RunOnEnter();
                    break;
                }
            }
        }

        private void RunOnEnter()
        {
            foreach (var behaviour in CurrentState.behaviours)
            {
                behaviour.OnStateEnter(this);
            }
            
            onStateEnter.Invoke(CurrentState.name);
        }

        private void RunOnUpdate()
        {
            foreach (var behaviour in CurrentState.behaviours)
            {
                behaviour.OnStateUpdate(this);
            }
        }

        private void RunOnExit()
        {
            foreach (var behaviour in CurrentState.behaviours)
            {
                behaviour.OnStateExit(this);
            }
            
            onStateExit.Invoke(CurrentState.name);
        }

        public uint GetStateIndex(string stateName)
        {
            var index = Array.FindIndex(graph.states, state => state.name == stateName);
            if (index < 0) throw new ArgumentException($"State '{stateName}' not found");
            
            return (uint)index;
        }
    }
}