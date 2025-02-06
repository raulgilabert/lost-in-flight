using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GlobalState
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Player.Player player;
        public bool IsPaused { get; private set; }
        public UnityEvent<bool> onTogglePause;

        private InputAction _pauseAction;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        
            _pauseAction = InputSystem.actions.FindAction("Pause");
        }

        private void Update()
        {
            if (_pauseAction.WasPerformedThisFrame()) TogglePause();
        }

        public void TogglePause()
        {
            if (player.GetComponent<Health.Health>().IsDead) return;
        
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0 : 1;
            onTogglePause.Invoke(IsPaused);
        }
    }
}
