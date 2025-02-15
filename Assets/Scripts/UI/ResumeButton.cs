using GlobalState;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResumeButton : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.Instance.resumeButton = this.GetComponent<Button>();
        }
    }
}