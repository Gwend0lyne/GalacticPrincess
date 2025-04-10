using System;
using UnityEngine;
using TMPro; 

public class InteractionTrigger : MonoBehaviour
{
    public event Action OnInteract;
    private bool isPlayerInside = false;
    [SerializeField] private GameObject interactionText;
    public RonronBar ronronBar;
    public PlayerControllerFlatWorld playerController;

    private void Start()
    {
        if (interactionText != null) interactionText.SetActive(false);
        ronronBar = FindObjectOfType<RonronBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with: " + other.name);
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (interactionText != null) interactionText.SetActive(true);
        }
        playerController = other.GetComponentInParent<PlayerControllerFlatWorld>();
        if (playerController == null)
        {
            Debug.LogError("PlayerControllerFlatWorld not found on the player object!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (interactionText != null) interactionText.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract?.Invoke();
            if (ronronBar != null)
            {
                ronronBar.IncreaseValue(10f); // Augmente la barre
            }
            
            if (playerController != null)
            {
                playerController.TriggerInteraction();
            }
            else
            {
                Debug.Log("ezez");
            }
        }
    }
}