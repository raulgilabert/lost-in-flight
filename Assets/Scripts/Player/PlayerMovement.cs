using Physics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float baseMovementSpeed;
        [SerializeField] private float slipperynessFactor;
        [SerializeField] private float jumpForce;

        public UnityEvent onJump;
        
        private Player _player;
        private Rigidbody2D _rigidbody;
        private GroundDetector _groundDetector;
        
        private InputAction _moveAction;
        private InputAction _jumpAction;
        
        private bool _jumpPressedLastUpdate;
        private int _jumpCount;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _groundDetector = GetComponent<GroundDetector>();

            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
        }

        private void Start()
        {
            _jumpPressedLastUpdate = false;
            _jumpCount = 0;
        }

        private void Update()
        {
            _jumpPressedLastUpdate = _jumpPressedLastUpdate || _jumpAction.WasPressedThisFrame();
        }

        private void FixedUpdate()
        {
            if (_groundDetector.IsGrounded) _jumpCount = 2;
            
            float currentSpeed = _rigidbody.linearVelocity.x;
            float targetSpeed = baseMovementSpeed * _moveAction.ReadValue<float>();
            float newSpeed;
        
            if (Mathf.Abs(targetSpeed) < Mathf.Abs(currentSpeed))
            {
                newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 1f / Mathf.Max(1, slipperynessFactor * _player.soapyness + 1));
            }
            else
            {
                newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.5f);
            }
            
            float currentVerticalSpeed = _rigidbody.linearVelocity.y;
            float newVerticalSpeed = currentVerticalSpeed;

            if (_jumpAction.IsPressed() && _jumpCount > 0 && (_jumpPressedLastUpdate || _groundDetector.IsGrounded))
            {
                newVerticalSpeed = jumpForce;
                --_jumpCount;
                onJump.Invoke();
            }

            _rigidbody.linearVelocity = new Vector2(newSpeed, newVerticalSpeed);
            
            _jumpPressedLastUpdate = false;
        }
    }
}