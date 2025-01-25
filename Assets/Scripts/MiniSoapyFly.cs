using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSoapyFly : MonoBehaviour
{
    private static readonly int Explode = Animator.StringToHash("Explode");
    
    public float speed;
    public bool flyRight;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _spriteRenderer.flipX = flyRight;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = (flyRight ? Vector2.right : Vector2.left) * speed;
    }

    public void OnDamageDealt()
    {
        speed = 0;
        _animator.SetTrigger(Explode);
    }

    public void OnExplosionEnd()
    {
        Destroy(gameObject);
    }
}
