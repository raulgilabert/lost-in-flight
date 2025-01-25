using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            damageReceiver.ReceiveDamage(damage);
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
            
            damageReceiver.ReceiveDamage(damage);
            onDamageDealt.Invoke(damage);
        }
    }

    private void Update()
    {
        _damageTimer -= Time.deltaTime;
    }
}
