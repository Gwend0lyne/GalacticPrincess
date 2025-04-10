using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerFlatWorld : MonoBehaviour
{
    private Vector2 _input;
    private Rigidbody _rb; 
    private HealthBar healthBar;
    public bool isInteracting = false;

    public float speed = 15f; // Vitesse de déplacement
    public float rotationSpeed = 200f; // Vitesse de rotation (contrôle la fluidité du pivotement)
    public float maxSpeedMultiplier = 2f;
    private Animator _animator;
    
    public Camera mainCamera;
    public Transform targetObject;
    public float smoothSpeed = 0.09f;

    private InteractableObject currentInteractable;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar = FindObjectOfType<HealthBar>();
        _animator = GetComponent<Animator>();
        
        initialCameraPosition = mainCamera.transform.localPosition;
        initialCameraRotation = mainCamera.transform.localRotation;
        _animator.SetBool("isInteracting", false);
    }

    void Update()
    {
        if (isInteracting)
        {
            Debug.Log("Interacting");

            // Calculer le déplacement de la caméra sur l'axe X de -20f à 20f
            // Vous pouvez utiliser un lerp pour obtenir un mouvement fluide sur cet axe
            float timeElapsed = Mathf.PingPong(Time.time * smoothSpeed, 1); // PingPong pour un aller-retour continu
            float newXPosition = Mathf.Lerp(-40f, 40f, timeElapsed);

            // Maintenir la position de la caméra sur l'axe Y et Z comme vous le souhaitez
            Vector3 desiredPosition = new Vector3(newXPosition, 40f, -70f);  // Décalage initial sur Y et Z
            Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.localPosition, desiredPosition, smoothSpeed);

            // Appliquer la position lissée à la caméra
            mainCamera.transform.localPosition = smoothedPosition;
            mainCamera.transform.LookAt(targetObject);

            // Vérifier si l'animation de danse est terminée
            if (IsDanceAnimationFinished())
            {
                // Réinitialiser isInteracting, la caméra et passer à Idle
                isInteracting = false;
                _animator.SetBool("isInteracting", false); // Revenir à Idle
                resetCamera();
            }

            return;
        }

        // Prise de l'entrée utilisateur via ZQSD pour déplacer le joueur.
        float inputX = Input.GetAxisRaw("Horizontal"); // Q/D
        float inputY = Input.GetAxisRaw("Vertical"); // Z/S

        _input = new Vector2(inputX, inputY).normalized; // Normaliser pour éviter les mouvements diagonaux trop rapides
    
        float speedMultiplier = 1f + (healthBar.GetValue() * (maxSpeedMultiplier - 1f));
        float currentSpeed = speed * speedMultiplier;
        
        // Calcul de la direction du mouvement basée sur l'orientation du joueur (local)
        Vector3 moveDirection = transform.forward * _input.y + transform.right * _input.x; // Déplacement basé sur la direction du joueur

        // Déplacement du joueur
        if (_input.magnitude > 0.1f) // Si une touche de mouvement est pressée
        {
            // Déplacement fluide du joueur (mouvement sur X et Z)
            Vector3 targetVelocity = moveDirection * currentSpeed;
            _rb.velocity = new Vector3(targetVelocity.x, 0, targetVelocity.z);
            _animator.SetBool("isRunning", true);
        }
        else
        {
            // Si aucune touche de déplacement n'est appuyée, arrêter le mouvement horizontal
            _rb.velocity = new Vector3(0, 0, 0);
            _animator.SetBool("isRunning", false);
        }

        // Gestion de la rotation (Q/D pour pivoter à gauche ou à droite)
        if (inputX != 0) // Si Q ou D est appuyé
        {
            // Pivoter en continu selon l'axe Y (gauche ou droite)
            float rotationAmount = inputX * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0); // Rotation fluide autour de l'axe Y
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Afficher un message lorsque quelque chose entre dans le trigger
        Debug.Log("Un objet entre dans la zone de trigger : " + other.gameObject.name);

        // Assurer que l'objet qui entre dans la zone de trigger a un InteractionTrigger
        InteractionTrigger trigger = other.GetComponentInParent<InteractionTrigger>();
    
        // Vérifier si un InteractionTrigger a été trouvé dans les parents de l'objet
        if (trigger != null)
        {
            Debug.Log("InteractionTrigger trouvé dans le parent de l'objet : " + other.gameObject.name);

            // Essayer d'obtenir l'objet InteractableObject depuis le parent
            currentInteractable = other.GetComponentInParent<InteractableObject>();

            // Vérifier si cet objet a le tag "Interactable"
            if (currentInteractable != null)
            {
                Debug.Log("InteractableObject trouvé : " + currentInteractable.gameObject.name);

                if (currentInteractable.CompareTag("Interactable"))
                {
                    Debug.Log("L'objet a le tag 'Interactable'. C'est un objet valide.");
                
                    // Définir l'objet cible pour la caméra
                    targetObject = currentInteractable.transform;
                }
                else
                {
                    Debug.Log("L'objet n'a pas le tag 'Interactable'. Ignoré.");
                }
            }
            else
            {
                Debug.Log("Aucun InteractableObject trouvé dans le parent.");
            }
        }
        else
        {
            Debug.Log("Aucun InteractionTrigger trouvé dans le parent de l'objet.");
        }
    }




    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            // Si le joueur quitte la zone de l'objet interactif
            currentInteractable = null;
            targetObject = null; // Retirer l'objet cible
        }
    }
    
    
    // Cette fonction est appelée lorsqu'une collision entre le joueur et un autre objet est détectée.
    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le joueur a touché un cube spécifique (par exemple avec un tag)
        if (collision.gameObject.CompareTag("Cube"))
        {
            // Change de scène si la collision a lieu avec un objet ayant le tag "Cube"
            SceneManager.LoadScene("Planete1_outside"); 
        }
    }
    public void TriggerInteraction()
    {
        isInteracting = true;
        _animator.SetBool("isInteracting", true);
    }
    
    private bool IsDanceAnimationFinished()
    {
        // Récupérer l'état actuel de l'animation de danse
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        // Vérifier si l'animation de danse est en cours
        // Cela suppose que l'animation de danse a un nom ou un tag spécifique pour la distinguer
        if (stateInfo.IsName("Dance")) // Remplacez "Dance" par le nom exact de votre animation
        {
            // Si l'animation est terminée, l'état est à la fin (normalizedTime >= 1)
            Debug.Log("Dance animation finished");
            return stateInfo.normalizedTime >= 1f;
        }

        return false;
    }
    
    private void resetCamera()
    {
        Debug.Log("resetCamera");
        mainCamera.transform.localPosition = initialCameraPosition;
        Debug.Log(mainCamera.transform.localPosition);
        mainCamera.transform.localRotation = initialCameraRotation;
    }
}