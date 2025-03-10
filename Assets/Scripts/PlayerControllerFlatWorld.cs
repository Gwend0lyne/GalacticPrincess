using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerFlatWorld : MonoBehaviour
{
    private Vector2 _input;
    private Rigidbody _rb; 

    public float speed = 15f; // Vitesse de déplacement
    public float rotationSpeed = 200f; // Vitesse de rotation (contrôle la fluidité du pivotement)

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Prise de l'entrée utilisateur via ZQSD pour déplacer le joueur.
        float inputX = Input.GetAxisRaw("Horizontal"); // Q/D
        float inputY = Input.GetAxisRaw("Vertical"); // Z/S

        _input = new Vector2(inputX, inputY).normalized; // Normaliser pour éviter les mouvements diagonaux trop rapides

        // Calcul de la direction du mouvement basée sur l'orientation du joueur (local)
        Vector3 moveDirection = transform.forward * _input.y + transform.right * _input.x; // Déplacement basé sur la direction du joueur

        // Déplacement du joueur
        if (_input.magnitude > 0.1f) // Si une touche de mouvement est pressée
        {
            // Déplacement fluide du joueur (mouvement sur X et Z)
            Vector3 targetVelocity = moveDirection * speed;
            _rb.velocity = new Vector3(targetVelocity.x, 0, targetVelocity.z);
        }
        else
        {
            // Si aucune touche de déplacement n'est appuyée, arrêter le mouvement horizontal
            _rb.velocity = new Vector3(0, 0, 0);
        }

        // Gestion de la rotation (Q/D pour pivoter à gauche ou à droite)
        if (inputX != 0) // Si Q ou D est appuyé
        {
            // Pivoter en continu selon l'axe Y (gauche ou droite)
            float rotationAmount = inputX * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0); // Rotation fluide autour de l'axe Y
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
}