using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soapy : MonoBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");
    
    private Rigidbody2D _rb;
    private Animator _animator;
    
    public float velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(0, -velocity, 0);
    }

    public void OnDealtDamage(float damage)
    {
        velocity = 0;
        _animator.SetTrigger(Death);
    }

    public void OnDeathEnd()
    {
        Destroy(gameObject);
    }
}
