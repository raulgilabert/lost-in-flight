using System;
using Player;
using UnityEngine;

namespace AnimationTriggers
{
    public class PlayerJumpAnimationTrigger : MonoBehaviour
    {
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            GetComponent<PlayerMovement>().onJump.AddListener(OnJump);
        }

        private void OnJump()
        {
            _animator.SetTrigger(Jump);
        }
    }
}