using System;
using UnityEngine;

namespace AnimationTriggers
{
    public class SpeedAnimationTrigger : MonoBehaviour
    {
        private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");

        [SerializeField] private bool horizontalSpeed = true;
        [SerializeField] private bool verticalSpeed = true;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (horizontalSpeed) _animator.SetFloat(HorizontalSpeed, _rigidbody.linearVelocityX);
            if (verticalSpeed) _animator.SetFloat(VerticalSpeed, _rigidbody.linearVelocityY);
        }
    }
}