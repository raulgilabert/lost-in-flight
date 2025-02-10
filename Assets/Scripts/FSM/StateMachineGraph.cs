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
        }
    }
}