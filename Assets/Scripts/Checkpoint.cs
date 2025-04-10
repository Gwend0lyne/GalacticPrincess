using UnityEngine;
using UnityEngine.Events; 
public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;
    void OnTriggerEnter(Collider collider)
    {
        onCheckpointEnter.Invoke(collider.gameObject, this);
       
    }
}
