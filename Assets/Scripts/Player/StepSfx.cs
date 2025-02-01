using Physics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class StepSfx : MonoBehaviour
    {
        private GroundDetector _groundDetector;
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;

        [SerializeField] private float horizontalVelocityThreshold;
        [SerializeField] private float stepCadence;
        [SerializeField] private float maxStepCadenceOffset;
        [SerializeField] private float maxPitchOffset;

        private float _stepTimer;
        
        void Start()
        {
            _groundDetector = GetComponentInParent<GroundDetector>();
            _rigidbody = GetComponentInParent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!_groundDetector.IsGrounded) return;
            
            TickStepTimer();
            RestartTimer();
        }

        private void TickStepTimer()
        {
            // Timer must be active
            if (_stepTimer <= 0) return;
            
            _stepTimer -= Time.deltaTime;
            
            // Perform end action
            if (_stepTimer <= 0)
            {
                _audioSource.pitch = Random.Range(1f - maxPitchOffset, 1f + maxPitchOffset);
                _audioSource.PlayOneShot(_audioSource.clip);
            }
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
