using GlobalState;
using UnityEngine;

namespace UI
{
    public class ResumeButton : MonoBehaviour
    {
        public void Resume()
        {
            GameManager.Instance.TogglePause();
        }
    }
}