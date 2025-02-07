using UnityEngine;
using UnityEngine.Events; 
public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;
    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.gameObject.tag == "Player")
        {
            // fire an event giving the entering gameObject and this checkpoint
            Debug.Log("Checkpoint entered");
            onCheckpointEnter.Invoke(collider.gameObject, this);
        }
    }
}
