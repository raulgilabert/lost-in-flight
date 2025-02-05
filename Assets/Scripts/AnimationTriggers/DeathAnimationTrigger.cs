using UnityEngine;

namespace AnimationTriggers
{
    public class DeathAnimationTrigger : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("Death");
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            GetComponent<Health.Health>().onDeath.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(Death);
        }
    }
}