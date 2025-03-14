using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        InteractionTrigger trigger = GetComponentInChildren<InteractionTrigger>();

        if (trigger != null)
        {
            trigger.OnInteract += Interact; 
        }
    }

    private void Interact()
    {
        Debug.Log("Objet interagi !");
        rend.material.color = Color.green; 
    }
}