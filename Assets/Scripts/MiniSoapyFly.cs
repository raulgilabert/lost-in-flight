using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniSoapyFly : MonoBehaviour
{
    private static readonly int Explode = Animator.StringToHash("Explode");
    
    public float speed;
    public bool flyRight;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.Play();
    }

    public void OnExplosionEnd()
    {
        Destroy(gameObject);
    }
}
