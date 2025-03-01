using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private bool continuous;
        [SerializeField] private float cadence;
        [SerializeField] private float damage;
        [SerializeField] private UnityEvent<float> onDamageDealt;

        private float _damageTimer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _damageTimer = 0;
            if (continuous) return;
        
            if (other.TryGetComponent(out DamageReceiver damageReceiver))
            {
                if (damageReceiver.ReceiveDamage(damage))
                    onDamageDealt.Invoke(damage);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!continuous) return;

            if (other.TryGetComponent(out DamageReceiver damageReceiver))
            {
                if (_damageTimer > 0) return;
                _damageTimer = cadence;

                if (damageReceiver.ReceiveDamage(damage))
                    onDamageDealt.Invoke(damage);
            }
        }

        private void Update()
        {
            _damageTimer -= Time.deltaTime;
        }
    }
}
