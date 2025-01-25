using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float _inputSpeed;
    private bool _inputJump;
    
    private bool _isGrounded;
    private int _jumpCount;
    private bool _jumpHeld;

    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float slipperyness;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundedRaycastDistance;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _inputSpeed = 0;
        _inputJump = false;
        _isGrounded = false;
        _jumpCount = 0;
        _jumpHeld = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundedRaycastDistance, _isGrounded ? Color.green : Color.red);

        if (_isGrounded) _jumpCount = 2;
        
        float currentSpeed = _rigidbody.velocity.x;
        float targetSpeed = baseMoveSpeed * _inputSpeed;
        float newSpeed;
        
        if (currentSpeed * targetSpeed < 0 || targetSpeed == 0)
        {
            newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 1f / Mathf.Max(1, slipperyness));
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
        }

        _rigidbody.velocity = new Vector2(newSpeed, newVerticalSpeed);
    }

    private void OnMove(InputValue value)
    {
        _inputSpeed = value.Get<float>();
    }

    private void OnJump(InputValue value)
    {
        _inputJump = value.Get<float>() > 0;
        if (!_inputJump)
        {
            _jumpHeld = false;
        }
    }
}
