using Physics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Death = Animator.StringToHash("Death");

        private PlayerMovement _playerMovement;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private AudioSource _audioSource;
        private GroundDetector _groundDetector;

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
        
        public bool IsDead { get; private set; }
    
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _groundDetector = GetComponent<GroundDetector>();
            
            GameManager.Instance.player = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            IsDead = false;
            
            _playerMovement.onJump.AddListener(OnJump);
            _groundDetector.onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void Update()
        {
            sprite.color = new Color(soapyColor.r, soapyColor.g, soapyColor.b, soapyColor.a * soapyness);

            if (IsDead)
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

            _animator.SetFloat(HorizontalSpeed, _rigidbody.linearVelocityX);

            if (Mathf.Abs(_rigidbody.linearVelocityX) > 0.1f)
            {
                sprite.flipX = _rigidbody.linearVelocityX < 0;
            }
        }

        private void OnJump()
        {
            _animator.SetTrigger(Jump);
            
            jumpAudioSource.pitch = Random.Range(0.9f, 1.1f);
            jumpAudioSource.Play();
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
                IsDead = true;
                _playerMovement.enabled = false;
            
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
    }
}