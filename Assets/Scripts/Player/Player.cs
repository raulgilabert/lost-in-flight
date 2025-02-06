using GlobalState;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("Death");

        [SerializeField] private Animator deathScreenAnimator;

        private void Awake()
        {
            GameManager.Instance.player = this;
        }

        public void OnDeathAnimationEnded()
        {
            deathScreenAnimator.SetTrigger(Death);
        }
    }
}