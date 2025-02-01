using System;
using System.Collections;
using System.Collections.Generic;
using Physics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Death = Animator.StringToHash("Death");

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private AudioSource _audioSource;
        private GroundDetector _groundDetector;

        private float _inputSpeed;
        private bool _inputJump;
        
        private int _jumpCount;
        private bool _jumpHeld;
        private float _stepTimer;
        private bool _isDead;

        private bool _isPaused;

        [SerializeField] private float baseMoveSpeed;
        [SerializeField] private float slipperynessFactor;
        [SerializeField] private float jumpForce;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color soapyColor;
        public float soapyness;
        [SerializeField] private float damageToSoapynessFactor;
        [SerializeField] private AudioSource jumpAudioSource;
        [SerializeField] private AudioSource hurtAudioSource;
        [SerializeField] private AudioSource deathAudioSource;
        [SerializeField] private ParticleSystem groundParticles;
        [SerializeField] private Animator deathScreenAnimator;
        [SerializeField] private GameObject deathUIFocus;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject pauseUIFocus;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _groundDetector = GetComponent<GroundDetector>();
        
            GameManager.Instance.player = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _inputSpeed = 0;
            _inputJump = false;
            _jumpCount = 0;
            _jumpHeld = false;
            _isDead = false;
            
            _groundDetector.onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void Update()
        {
            sprite.color = new Color(soapyColor.r, soapyColor.g, soapyColor.b, soapyColor.a * soapyness);

            if (_isDead)
            {
                _animator.SetTrigger(Death);
            }
        }

        private void FixedUpdate()
        {
            _animator.SetBool(Grounded, _groundDetector.IsGrounded);
            var groundParticlesEmission = groundParticles.emission;
            groundParticlesEmission.enabled = _groundDetector.IsGrounded;
            groundParticlesEmission.rateOverDistanceMultiplier = 1 + 2 * soapyness;

            if (_groundDetector.IsGrounded) _jumpCount = 2;
        
            float currentSpeed = _rigidbody.linearVelocity.x;
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
        
            float currentVerticalSpeed = _rigidbody.linearVelocity.y;
            float newVerticalSpeed = currentVerticalSpeed;

            if (_inputJump && _jumpCount > 0 && (!_jumpHeld || _groundDetector.IsGrounded))
            {
                newVerticalSpeed = jumpForce;
                --_jumpCount;
                _jumpHeld = true;
                _animator.SetTrigger(Jump);
            
                jumpAudioSource.pitch = Random.Range(0.9f, 1.1f);
                jumpAudioSource.Play();
            }

            _rigidbody.linearVelocity = new Vector2(newSpeed, newVerticalSpeed);
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
            _inputJump = value.Get<float>() > 0 && !_isPaused;
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
            EventSystem.current.SetSelectedGameObject(deathUIFocus);
        }

        public void OnGroundedStateChange(bool grounded)
        {
            if (grounded)
            {
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.Play();
            }
        }

        public void Pause()
        {
            if (_isDead) return;

            _isPaused = !_isPaused;

            Time.timeScale = (_isPaused ? 0 : 1);
            pauseUI.SetActive(_isPaused);

            if (pauseUI.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(pauseUIFocus);
            }
        }

        public void OnPause(InputValue value)
        {
            Pause();
        }
    }
}