using System.Collections;
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
        
        private bool _coroutineRunning;
        
        private new void Awake()
        {
            base.Awake();
            
            _groundDetector = GetComponentInParent<GroundDetector>();
            _rigidbody = GetComponentInParent<Rigidbody2D>();
            
            _groundDetector.onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void OnEnable()
        {
            if (_groundDetector.IsGrounded)
            {
                StartCoroutine(TriggerLoop());
                _coroutineRunning = true;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _coroutineRunning = false;
        }

        private void OnGroundedStateChange(bool grounded)
        {
            if (grounded)
            {
                if (!_coroutineRunning) StartCoroutine(TriggerLoop()); 
            }
            else
            {
                if (_coroutineRunning) StopAllCoroutines();
            }
            
            _coroutineRunning = grounded;
        }

        private IEnumerator TriggerLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(stepCadence - maxStepCadenceOffset, stepCadence + maxStepCadenceOffset));
                yield return new WaitUntil( () => Mathf.Abs(_rigidbody.linearVelocityX) > horizontalVelocityThreshold );
                
                Play();
            }
        }
    }
}
