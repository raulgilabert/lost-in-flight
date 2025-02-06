using UnityEngine;

namespace UI
{
    public class PlayButton : MonoBehaviour
    {
        private static readonly int Fade = Animator.StringToHash("Fade");
    
        [SerializeField] private Animator fadeAnimator;
    
        public void OnPressPlay()
        {
            fadeAnimator.SetTrigger(Fade);
        }
    }
}
