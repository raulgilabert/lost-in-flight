using System;
using UnityEngine;
using UnityEngine.Events;

namespace Physics
{
    public class GroundDetector : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float raycastDistance;
        public UnityEvent<bool> onGroundedStateChange;
        
        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            bool old = IsGrounded;
            IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
            
            if (old != IsGrounded) onGroundedStateChange.Invoke(IsGrounded);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
            Gizmos.DrawSphere(transform.position + Vector3.down * raycastDistance, .01f);
        }
    }
}