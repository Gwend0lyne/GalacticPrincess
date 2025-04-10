using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _cam;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private float _speed = 30;
    [SerializeField] private float _turnSpeed = 1500f;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private GravityBody _gravityBody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravityBody = GetComponent<GravityBody>();
    }

    void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        bool isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
        _animator.SetBool("isJumping", !isGrounded); // Optionnel si plus de saut
    }

    void FixedUpdate()
    {
        bool isRunning = _direction.magnitude > 0.1f;

        if (isRunning)
        {
            Vector3 direction = transform.forward * _direction.z;
            _rigidbody.MovePosition(_rigidbody.position + direction * (_speed * Time.fixedDeltaTime));

            Quaternion rightDirection = Quaternion.Euler(0f, _direction.x * (_turnSpeed * Time.fixedDeltaTime), 0f);
            Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection,
                Time.fixedDeltaTime * 3f);
            _rigidbody.MoveRotation(newRotation);
        }
        ApplyDownforce(); 
    }

    void ApplyDownforce()
        {
            float downforce = 1000f;
            _rigidbody.AddForce(_gravityBody.GravityDirection * downforce);
        }
    }
