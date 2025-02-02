using Physics;
using UnityEngine;

namespace SFX
{
    public class StepSfx : SfxTrigger
    {
        private GroundDetector _groundDetector;
        private Rigidbody2D _rigidbody;

        [SerializeField] private float horizontalVelocityThreshold;
        [SerializeField] private float stepCadence;
        [SerializeField] private float maxStepCadenceOffset;

        private float _stepTimer;
        
        private new void Start()
        {
            base.Start();
            
            _groundDetector = GetComponentInParent<GroundDetector>();
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_groundDetector.IsGrounded)
            {
                _stepTimer = 0;
                return;
            }
            
            TickStepTimer();
            RestartTimer();
        }

        private void TickStepTimer()
        {
            // Timer must be active
            if (_stepTimer <= 0) return;
            
            _stepTimer -= Time.deltaTime;
            
            // Perform end action
            if (_stepTimer <= 0) Play();
        }

        private void RestartTimer()
        {
            // Timer must be inactive
            if (_stepTimer > 0) return;

            if (Mathf.Abs(_rigidbody.linearVelocityX) > horizontalVelocityThreshold)
            {
                _stepTimer =  + Random.Range(stepCadence - maxStepCadenceOffset, stepCadence + maxStepCadenceOffset);
            }
        }
    }
}
