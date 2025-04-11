
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIControls : MonoBehaviour
{
    private Vector2 input;
    public UnityEvent<Vector2> onInput;

    public Transform waypointsHolder;
    private List<Transform> waypoints;
    private Transform nextWaypoint;
    private Vector3 nextWaypointPosition;

    public float maxDistanceToTarget = 5f;
    public float maxDistanceToReverse = 10f;

    public float randomJitterOnPosition = .5f;
    
    public bool canMove = false;
    void Awake()
    {
        // TODO : Initialize waypoints list by finding all Transform waypoints inside waypointsHolder
        waypoints = new List<Transform>();

        // Get all children of waypointsHolder and filter out the waypointsHolder itself
        foreach (Transform child in waypointsHolder)
        {
            waypoints.Add(child);
        }
    }

    void Start()
    {
        // Start with first waypoint
        SelectWaypoint(waypoints[0]);
    }

    void Update()
    {
        if (!canMove)
        {
            onInput?.Invoke(Vector2.zero); // force IA à rester immobile
            return;
        }
        Debug.Log("canMove");
        // ... (reste du code normal ici)
        
        // Change to next waypoint if reached current waypoint
        float distanceToTarget = Vector3.Distance(transform.position, nextWaypointPosition);
        if (distanceToTarget < maxDistanceToTarget)
        {
            int nextIndex = waypoints.IndexOf(nextWaypoint) + 1;
            SelectWaypoint(nextIndex < waypoints.Count ? waypoints[nextIndex] : waypoints[0]);
        }

        // Compute Vector2 input based on distances in Right and Forward axis
        Vector3 diff = nextWaypointPosition - transform.position;
        float componentForward = Vector3.Dot(diff, transform.forward.normalized);
        float componentRight = Vector3.Dot(diff, transform.right.normalized);
        input = new Vector2(componentRight, componentForward).normalized;

        // If target behind but too far, turn around
        if (componentForward < 0 && distanceToTarget > maxDistanceToReverse)
        {
            input.y = 1f;
            input.x = Mathf.Sign(componentRight) * 1f;
        }
        onInput?.Invoke(input);
    }

    void SelectWaypoint(Transform waypoint)
    {
        nextWaypoint = waypoint;
        // Totally optional : 
        // This "jitter" add a little randomness around the waypoint to make the AI slightly more human 
        nextWaypointPosition = nextWaypoint.position + new Vector3(Random.Range(-randomJitterOnPosition, randomJitterOnPosition), 0, Random.Range(-randomJitterOnPosition, randomJitterOnPosition));
    }
    
    
}
