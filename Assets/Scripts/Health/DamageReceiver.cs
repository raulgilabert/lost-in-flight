using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private float damageMultiplier = 1f;
        public UnityEvent<float> onDamageReceived;
    
        public void ReceiveDamage(float damage)
        {
            onDamageReceived.Invoke(damage * damageMultiplier);
        }
    }
}
