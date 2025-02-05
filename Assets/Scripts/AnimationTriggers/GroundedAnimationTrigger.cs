using Physics;
using UnityEngine;

namespace AnimationTriggers
{
    public class GroundedAnimationTrigger : MonoBehaviour
    {
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            GetComponent<GroundDetector>().onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void OnGroundedStateChange(bool grounded)
        {
            _animator.SetBool(Grounded, grounded);
        }
    }
}