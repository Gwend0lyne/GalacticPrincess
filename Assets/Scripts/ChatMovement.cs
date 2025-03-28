using UnityEngine;

public class ChatMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _turnSpeed = 2500f;
    [SerializeField] private float jumpForce = 7000f;
    public float downforce = 100f;
    [SerializeField] private LayerMask groundLayer;
    
    
    private Rigidbody _rigidbody;
    private Vector2 _input;
    private GravityBody _gravityBody;
    private bool _isGrounded = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravityBody = GetComponent<GravityBody>();

        // Active une meilleure détection des collisions
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        bool isRunning = _input.magnitude > 0.1f;

        if (isRunning)
        {
            // Définir une direction qui inclut la gravité
            Vector3 forwardDirection = Vector3.ProjectOnPlane(transform.forward, -_gravityBody.GravityDirection).normalized;
            
            // Appliquer la vitesse pour le déplacement avant/arrière
            Vector3 velocity = forwardDirection * (_input.y * _speed);
            velocity += Vector3.Project(_rigidbody.velocity, -_gravityBody.GravityDirection); // Garder la composante verticale
            _rigidbody.velocity = velocity;


            // Appliquer la rotation avec un effet fluide
            Quaternion rightDirection = Quaternion.Euler(0f, _input.x * (_turnSpeed * Time.fixedDeltaTime), 0f);
            Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection, Time.fixedDeltaTime * 3f);
            _rigidbody.MoveRotation(newRotation);
        }
        ApplyDownforce();
        CheckGrounded(); 
    }

    void ApplyDownforce()
    {
        
        _rigidbody.AddForce(_gravityBody.GravityDirection * downforce, ForceMode.Acceleration);
    }

    // Méthode pour recevoir l'input
    public void SetInputs(Vector2 input)
    {
        _input = input;
    }
    
    public void Jump()
    {
        if (_isGrounded) // Vérifie si le joueur est au sol
        {
            Debug.Log("Jumping");
            _rigidbody.AddForce(-_gravityBody.GravityDirection * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    
    void CheckGrounded()
    {
        float rayDistance = 3.2f; 
        if (Physics.Raycast(transform.position, _gravityBody.GravityDirection, out RaycastHit hit, rayDistance, groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        Debug.DrawRay(transform.position, _gravityBody.GravityDirection * rayDistance, _isGrounded ? Color.green : Color.red);
    }
}