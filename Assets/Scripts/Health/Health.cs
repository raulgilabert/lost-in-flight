using System;
using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class Health : MonoBehaviour
    {
        public float health = 1f;
        public UnityEvent<float> onHealthChanged;
        public UnityEvent<float, bool> onDamaged;
        public UnityEvent onDeath;
        
        public bool IsDead => health <= 0;

        private void Start()
        {
            if (TryGetComponent(out DamageReceiver damageReceiver))
            {
                damageReceiver.onDamageReceived.AddListener(Damage);
            }
        }

        public void Damage(float damage)
        {
            health = Mathf.Max(0, health - damage);

            if (damage != 0)
            {
                onHealthChanged.Invoke(health);
                onDamaged.Invoke(damage, IsDead);
            }
            if (health <= 0) onDeath.Invoke();
        }
    }
}