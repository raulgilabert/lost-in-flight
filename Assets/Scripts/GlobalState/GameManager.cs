using FSM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlobalState
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private const string Paused = "Paused";

        public Player.Player player;
        public UnityEvent<bool> onTogglePause;
        public Button resumeButton;
        
        public bool IsPaused => _stateMachine.CurrentStateName == "Paused";

        private StateMachine _stateMachine;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        
            _stateMachine = GetComponent<StateMachine>();
        }

        private void Start()
        {
            _stateMachine.onStateEnter.AddListener(OnStateEnter);
            _stateMachine.onStateExit.AddListener(OnStateExit);
        }

        private void OnStateEnter(string stateName)
        {
            if (stateName == Paused) onTogglePause.Invoke(true);
        }

        private void OnStateExit(string stateName)
        {
            if (stateName == Paused) onTogglePause.Invoke(false);
        }
    }
}
