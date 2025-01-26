using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Death = Animator.StringToHash("Death");

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;

    private float _inputSpeed;
    private bool _inputJump;
    
    private bool _isGrounded;
    private int _jumpCount;
    private bool _jumpHeld;
    private float _stepTimer;
    private bool _isDead;

    private bool _isPaused;

    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float slipperynessFactor;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundedRaycastDistance;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float stepCadence;
    [SerializeField] private Color soapyColor;
    public float soapyness;
    [SerializeField] private float damageToSoapynessFactor;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioSource hurtAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private ParticleSystem groundParticles;
    [SerializeField] private Animator deathScreenAnimator;
    [SerializeField] private GameObject pauseUI;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        GameManager.Instance.player = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _inputSpeed = 0;
        _inputJump = false;
        _isGrounded = false;
        _jumpCount = 0;
        _jumpHeld = false;
        _isDead = false;
    }

    private void Update()
    {
        if (_stepTimer > 0)
        {
            _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0 && _isGrounded)
            {
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.Play();
            }
        }

        if (Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
        {
            if (_stepTimer <= 0) _stepTimer = stepCadence + Random.Range(-0.01f, +0.01f);
        }
        else
        {
            _stepTimer = 0;
        }

        sprite.color = new Color(soapyColor.r, soapyColor.g, soapyColor.b, soapyColor.a * soapyness);

        if (_isDead)
        {
            _animator.SetTrigger(Death);
        }
    }

    private void FixedUpdate()
    {
        bool oldIsGrounded = _isGrounded;
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundedRaycastDistance, _isGrounded ? Color.green : Color.red);
        _animator.SetBool(Grounded, _isGrounded);
        var groundParticlesEmission = groundParticles.emission;
        groundParticlesEmission.enabled = _isGrounded;
        groundParticlesEmission.rateOverDistanceMultiplier = 1 + 2 * soapyness;

        if (!oldIsGrounded && _isGrounded)
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.Play();
        }

        if (_isGrounded) _jumpCount = 2;
        
        float currentSpeed = _rigidbody.velocity.x;
        float targetSpeed = baseMoveSpeed * _inputSpeed;
        float newSpeed;
        
        if (Mathf.Abs(targetSpeed) < Mathf.Abs(currentSpeed))
        {
            newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 1f / Mathf.Max(1, slipperynessFactor * soapyness + 1));
        }
        else
        {
            newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.5f);
        }
        
        float currentVerticalSpeed = _rigidbody.velocity.y;
        float newVerticalSpeed = currentVerticalSpeed;

        if (_inputJump && _jumpCount > 0 && (!_jumpHeld || _isGrounded))
        {
            newVerticalSpeed = jumpForce;
            --_jumpCount;
            _jumpHeld = true;
            _animator.SetTrigger(Jump);
            
            jumpAudioSource.pitch = Random.Range(0.9f, 1.1f);
            jumpAudioSource.Play();
        }

        _rigidbody.velocity = new Vector2(newSpeed, newVerticalSpeed);
        _animator.SetFloat(HorizontalSpeed, newSpeed);

        if (Mathf.Abs(newSpeed) > 0.1f)
        {
            sprite.flipX = newSpeed < 0;
        }
    }

    private void OnMove(InputValue value)
    {
        if (_isDead) return;
        _inputSpeed = value.Get<float>();
    }

    private void OnJump(InputValue value)
    {
        if (_isDead) return;
        _inputJump = value.Get<float>() > 0;
        if (!_inputJump)
        {
            _jumpHeld = false;
        }
    }

    public void OnDamageReceived(float damage)
    {
        
        soapyness += damage * damageToSoapynessFactor;
        var groundParticlesMain = groundParticles.main;
        groundParticlesMain.startColor = soapyColor;

        if (soapyness >= 1)
        {
            _animator.SetTrigger(Death);
            Destroy(GetComponent<DamageReceiver>());
            baseMoveSpeed = 0;
            jumpForce = 0;
            _isDead = true;
            
            deathAudioSource.Play();
        }
        else
        {
            hurtAudioSource.pitch = Random.Range(0.9f, 1.1f);
            hurtAudioSource.Play();
        }
    }

    public void OnDeathAnimationEnded()
    {
        deathScreenAnimator.SetTrigger(Death);
    }

    public void Pause()
    {
        if (_isDead) return;

        _isPaused = !_isPaused;

        Time.timeScale = (_isPaused ? 0 : 1);
        pauseUI.SetActive(_isPaused);
    }

    public void OnPause(InputValue value)
    {
        Pause();
    }
}
