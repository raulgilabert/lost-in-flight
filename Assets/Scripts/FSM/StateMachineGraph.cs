using System;
using System.Linq;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(fileName = "StateMachineGraph", menuName = "FSM/State Machine Graph", order = 0)]
    public class StateMachineGraph : ScriptableObject
    {
        [Serializable]
        public struct State
        {
            public string name;
            [SerializeReference]
            public StateMachineBehaviour[] behaviours;
        }

        public State[] states;

        private void OnValidate()
        {
            UnityEngine.Assertions.Assert.AreEqual(states.Select(state => state.name).Distinct().Count(), states.Length, "State names should be unique");

            foreach (var state in states)
            {
                UnityEngine.Assertions.Assert.IsFalse(String.IsNullOrEmpty(state.name), "State name should not be empty");
                UnityEngine.Assertions.Assert.IsTrue(state.behaviours.All(b => b != null), $"State behaviours in [{state.name}] should not be null, please fill in or delete empty behaviours");
            }
        }
    }
}