using System;
using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private float damageMultiplier = 1f;
        public UnityEvent<float> onDamageReceived;
    
        public bool ReceiveDamage(float damage)
        {
            if (!isActiveAndEnabled) return false;
            
            onDamageReceived.Invoke(damage * damageMultiplier);
            return true;
        }
    }
}
