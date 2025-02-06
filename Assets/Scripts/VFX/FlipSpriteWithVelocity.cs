using System;
using UnityEngine;

namespace VFX
{
    public class FlipSpriteWithVelocity : MonoBehaviour
    {
        private enum Mode
        {
            Disabled,
            FlipWhenNegative,
            FlipWhenPositive,
        }

        [SerializeField] private Mode xMode;
        [SerializeField] private Mode yMode;
        [SerializeField] private float threshold = 0.1f;
        
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var velocity = _rigidbody.linearVelocity;

            if (Mathf.Abs(velocity.x) > threshold)
            {
                _spriteRenderer.flipX = xMode switch
                {
                    Mode.FlipWhenPositive => velocity.x > 0,
                    Mode.FlipWhenNegative => velocity.x < 0,
                    _ => _spriteRenderer.flipX
                };
            }

            if (Mathf.Abs(velocity.y) > threshold)
            {
                _spriteRenderer.flipY = yMode switch
                {
                    Mode.FlipWhenPositive => velocity.y > 0,
                    Mode.FlipWhenNegative => velocity.y < 0,
                    _ => _spriteRenderer.flipY
                };
            }
        }
    }
}