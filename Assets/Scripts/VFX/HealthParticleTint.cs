using UnityEngine;

namespace VFX
{
    public class HealthParticleTint : MonoBehaviour
    {
        [SerializeField] private Gradient tintGradient;
        
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            GetComponentInParent<Health.Health>().onHealthChanged.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(float health)
        {
            var particlesMain = _particleSystem.main;
            particlesMain.startColor = tintGradient.Evaluate(health);
        }
    }
}