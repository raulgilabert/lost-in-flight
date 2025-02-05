using Health;
using Physics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("Death");

        private PlayerMovement _playerMovement;
        private Health.Health _health;
        private Rigidbody2D _rigidbody;

        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color soapyColor;
        [SerializeField] private ParticleSystem groundParticles;
        [SerializeField] private Animator deathScreenAnimator;
        [SerializeField] private GameObject deathUIFocus;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject pauseUIFocus;
    
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _health = GetComponent<Health.Health>();
            _rigidbody = GetComponent<Rigidbody2D>();
            
            GameManager.Instance.player = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _health.onHealthChanged.AddListener(OnHealthChanged);
            _health.onDeath.AddListener(OnDeath);
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(_rigidbody.linearVelocityX) > 0.1f)
            {
                sprite.flipX = _rigidbody.linearVelocityX < 0;
            }
        }

        private void OnHealthChanged(float health)
        {
            var groundParticlesEmission = groundParticles.emission;
            groundParticlesEmission.rateOverDistanceMultiplier = 1 + 2 * (1 - health);
        }

        private void OnDeath()
        {
            Destroy(GetComponent<DamageReceiver>());
            _playerMovement.enabled = false;
        }

        public void OnDeathAnimationEnded()
        {
            deathScreenAnimator.SetTrigger(Death);
            EventSystem.current.SetSelectedGameObject(deathUIFocus);
        }
    }
}