using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class WallCollisionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer; // Set the "Walls" layer in the Inspector
    [SerializeField] private float maxClimbAngle = 45f; // Maximum angle that is climbable

    private Rigidbody rb;

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
        // Check if the object collided with has the wallLayer
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            // Reset velocity to stop traversing walls
            Debug.Log("Checkpoint entered");
            rb.velocity = Vector3.zero;
        }
    }
}