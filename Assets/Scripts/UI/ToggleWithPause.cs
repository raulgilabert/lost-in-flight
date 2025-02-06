using GlobalState;
using UnityEngine;

namespace UI
{
    public class ToggleWithPause : MonoBehaviour
    {
        [SerializeField] private bool reversed;

        private void Start()
        {
            GameManager.Instance.onTogglePause.AddListener(OnTogglePause);
            OnTogglePause(GameManager.Instance.IsPaused);
        }

        private void OnTogglePause(bool paused)
        {
            gameObject.SetActive(reversed ? !paused : paused);
        }
    }
}