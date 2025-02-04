using UnityEngine;

namespace Health
{
    public class HealthSpriteTint : MonoBehaviour
    {
        [SerializeField] private Gradient tintGradient;

        private SpriteRenderer _spriteRenderer;
        
        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GetComponentInParent<Health>().onHealthChanged.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(float health)
        {
            _spriteRenderer.color = tintGradient.Evaluate(health);
        }
    }
}