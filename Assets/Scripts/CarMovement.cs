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
    
    [SerializeField] private Transform _visualCar; // Drag ton enfant "Voiture" ici dans l’inspecteur
    [SerializeField] private float _driftAngle = 15f; // Max roll angle
    [SerializeField] private float _driftSmooth = 5f;

    void Start()
    {
        Debug.Log("_visualcar " + _visualCar.name);
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
        ApplyVisualDrift();
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
    
    void ApplyVisualDrift()
    {
        if (_visualCar == null) return;

        // Calcul d’un angle Z basé sur l’input horizontal
        float targetZRotation = -_input.x * _driftAngle; // négatif pour pencher du bon côté
        Quaternion targetRotation;
        
        if (_visualCar.name == "trueno ai")
        {
            targetRotation = Quaternion.Euler(-90f, 0f, -90f + targetZRotation);
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 0f, targetZRotation);
        }
        
        

        _visualCar.localRotation = Quaternion.Slerp(_visualCar.localRotation, targetRotation, Time.fixedDeltaTime * _driftSmooth);
    }
}