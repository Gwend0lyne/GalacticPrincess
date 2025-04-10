using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class WallCollisionHandler : MonoBehaviour
{
    //PAS UTILISER
    [SerializeField] private LayerMask wallLayer; // Set the "Walls" layer in the Inspector
    [SerializeField] private float maxClimbAngle = 45f; // Maximum angle that is climbable

    private Rigidbody rb;
    
    public PlayerCollector playerCollector;
    public UIBanner noPowerBanner;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PreventClimbingWalls();
        
    }

    void PreventClimbingWalls()
    {
        // Cast a ray downward to check for collisions with walls
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, wallLayer))
        {
            // Check the angle of the surface
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > maxClimbAngle)
            {
                // Prevent movement by resetting velocity
                
                rb.velocity = Vector3.zero;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            Debug.Log("Mur touché");
            rb.velocity = Vector3.zero;

            if (playerCollector != null && !playerCollector.HasPower())
            {
                if (noPowerBanner != null)
                    noPowerBanner.ShowBanner();
            }
        }
    }
}