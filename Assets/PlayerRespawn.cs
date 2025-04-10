using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;
    public LayerMask waterLayer;
    public float maxAirTime = 4f;

    private Rigidbody rb;
    private ChatMovement chatMovement;
    private float airTimer = 0f;
    private ScreenFade screenFade;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chatMovement = GetComponent<ChatMovement>();
        screenFade = FindObjectOfType<ScreenFade>();
    }

    void Update()
    {
        // Si le joueur n’est pas au sol, on incrémente le timer
        if (chatMovement != null && !chatMovement.IsGrounded())
        {
            airTimer += Time.deltaTime;

            if (airTimer >= maxAirTime)
            {
                Debug.Log("⏱ Trop longtemps dans les airs. Respawn !");
                Respawn();
            }
        }
        else
        {
            airTimer = 0f; // Reset si touché au sol
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & waterLayer) != 0)
        {
            Debug.Log("🌊 Touche l'eau → respawn");
            Respawn();
        }
    }

    void Respawn()
    {
        airTimer = 0f;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = respawnPoint.position;
        transform.rotation = Quaternion.identity;
        
        if (screenFade != null)
        {
            screenFade.PlayFade();
        }

    }
}