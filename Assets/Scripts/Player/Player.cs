using Health;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("Death");

        [SerializeField] private Animator deathScreenAnimator;
        [SerializeField] private GameObject deathUIFocus;

        private void Awake()
        {
            GameManager.Instance.player = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Health.Health>().onDeath.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            Destroy(GetComponent<DamageReceiver>());
            GetComponent<PlayerMovement>().enabled = false;
        }

        public void OnDeathAnimationEnded()
        {
            deathScreenAnimator.SetTrigger(Death);
            EventSystem.current.SetSelectedGameObject(deathUIFocus);
        }
    }
}