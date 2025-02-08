using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    
    //[SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private float _speed = 30;
    [SerializeField] private float _turnSpeed = 1500f;
    
    private Rigidbody _rigidbody;
    private Vector2 _input;
    private GravityBody _gravityBody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravityBody = GetComponent<GravityBody>();
    }

    void FixedUpdate()
    {
        bool isRunning = _input.magnitude > 0.1f;

        if (isRunning)
        {
            Vector3 direction = transform.forward * _input.y;
            _rigidbody.MovePosition(_rigidbody.position + direction * (_speed * Time.fixedDeltaTime));

            Quaternion rightDirection = Quaternion.Euler(0f, _input.x * (_turnSpeed * Time.fixedDeltaTime), 0f);
            Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection, Time.fixedDeltaTime * 3f);
            _rigidbody.MoveRotation(newRotation);
        }

        ApplyDownforce(); 
    }

    void ApplyDownforce()
    {
        float downforce = 1000f;
        _rigidbody.AddForce(_gravityBody.GravityDirection * downforce);
    }

    // Method to receive input
    public void SetInputs(Vector2 input)
    {
        _input = input;
    }
}