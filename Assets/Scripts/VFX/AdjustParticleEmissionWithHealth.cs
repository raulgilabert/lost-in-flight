using System;
using UnityEngine;

namespace VFX
{
    public class AdjustParticleEmissionWithHealth : MonoBehaviour
    {
        [SerializeField] private AnimationCurve emissionCurve;
        
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            GetComponentInParent<Health.Health>().onHealthChanged.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(float health)
        {
            var emission = _particleSystem.emission;
            emission.rateOverDistanceMultiplier = emissionCurve.Evaluate(health);
        }
    }
}