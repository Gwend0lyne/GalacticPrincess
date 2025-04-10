using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _turnSpeed = 1500f;
    public float downforce = 1000f;
    
    private Rigidbody _rigidbody;
    private Vector2 _input;
    private GravityBody _gravityBody;
    
    public bool canMove = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravityBody = GetComponent<GravityBody>();

        // Active une meilleure détection des collisions
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        
        bool isRunning = _input.magnitude > 0.1f;

        if (isRunning)
        {
            // Définir une direction qui inclut la gravité
            Vector3 forwardDirection = Vector3.ProjectOnPlane(transform.forward, -_gravityBody.GravityDirection).normalized;
            
            // Appliquer la vitesse pour le déplacement avant/arrière
            _rigidbody.velocity = forwardDirection * (_input.y * _speed);

            // Appliquer la rotation avec un effet fluide
            Quaternion rightDirection = Quaternion.Euler(0f, _input.x * (_turnSpeed * Time.fixedDeltaTime), 0f);
            Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection, Time.fixedDeltaTime * 3f);
            _rigidbody.MoveRotation(newRotation);
        }
        ApplyDownforce();
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
}