using System;

namespace FSM
{
    //[Serializable]
    public interface StateMachineBehaviour
    {
        public void Start(StateMachine stateMachine)
        {
            
        }
        
        public void OnStateEnter(StateMachine stateMachine)
        {
            // Do nothing by default
        }

        public void OnStateUpdate(StateMachine stateMachine)
        {
            // Do nothing by default
        }

        public void OnStateExit(StateMachine stateMachine)
        {
            // Do nothing by default
        }

        public bool ShouldTransition(StateMachine stateMachine, out uint nextState)
        {
            nextState = 0;
            return false;
        }

        public string GetLabel() => null;
    }
}