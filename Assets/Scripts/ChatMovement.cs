using UnityEngine;

public class ChatMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _turnSpeed = 2500f;
    [SerializeField] private float jumpForce = 7000f;
    public float downforce = 100f;
    [SerializeField] private LayerMask groundLayer;
    private Animator _animator;
    
    
    private Rigidbody _rigidbody;
    private Vector2 _input;
    private GravityBody _gravityBody;
    private bool _isGrounded = false;
    
    private Orbit currentOrbit;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravityBody = GetComponent<GravityBody>();
        _animator = GetComponent<Animator>();

        // Active une meilleure détection des collisions
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _animator.SetBool("isInteracting", false);
    }

    void FixedUpdate()
    {
        bool isRunning = _input.magnitude > 0.1f;

        if (isRunning)
        {
            _animator.SetBool("isRunning", true);
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
        else
        {
            _animator.SetBool("isRunning", false);
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

            Orbit orbit = hit.transform.GetComponentInParent<Orbit>();
            if (orbit != null)
            {
                // Si c’est une nouvelle planète, arrête sa rotation
                if (orbit != currentOrbit)
                {
                    // Redémarre l'ancienne planète si on en quitte une autre
                    if (currentOrbit != null)
                        currentOrbit.SetOrbitActive(true);

                    orbit.SetOrbitActive(false);
                    currentOrbit = orbit;

                    Debug.Log($"🚫 Arrêt de l'orbite : {orbit.name}");
                }
            }
            else if (currentOrbit != null)
            {
                // On est au sol, mais plus sur une planète avec Orbit → relancer l'ancienne si elle existe
                currentOrbit.SetOrbitActive(true);
                Debug.Log($"▶️ Reprise de l'orbite : {currentOrbit.name}");
                currentOrbit = null;
            }
        }
        else
        {
            _isGrounded = false;

            // Quand on n’est plus au sol, on relance la planète si nécessaire
            if (currentOrbit != null)
            {
                currentOrbit.SetOrbitActive(true);
                Debug.Log($"🕊️ Reprise de l'orbite : {currentOrbit.name}");
                currentOrbit = null;
            }
        }

        Debug.DrawRay(transform.position, _gravityBody.GravityDirection * rayDistance, _isGrounded ? Color.green : Color.red);
    }


    
    public bool IsGrounded()
    {
        return _isGrounded;
    }

}