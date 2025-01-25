using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> onDamageReceived;
    
    public void ReceiveDamage(float damage)
    {
        onDamageReceived.Invoke(damage);
    }
}
