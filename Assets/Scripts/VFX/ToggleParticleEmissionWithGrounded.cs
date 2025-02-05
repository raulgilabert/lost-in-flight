using Physics;
using UnityEngine;

namespace VFX
{
    public class ToggleParticleEmissionWithGrounded : MonoBehaviour
    {
        [SerializeField] private bool reversed;
        
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            var groundDetector = GetComponentInParent<GroundDetector>();
            
            groundDetector.onGroundedStateChange.AddListener(OnGroundedStateChange);
            OnGroundedStateChange(groundDetector.IsGrounded);
        }

        private void OnGroundedStateChange(bool grounded)
        {
            var emission = _particleSystem.emission;
            emission.enabled = (reversed ? !grounded : grounded);
        }
    }
}