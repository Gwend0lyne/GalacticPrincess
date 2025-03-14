using System;
using UnityEngine;
using TMPro; 

public class InteractionTrigger : MonoBehaviour
{
    public event Action OnInteract;
    private bool isPlayerInside = false;
    [SerializeField] private GameObject interactionText;
    public HealthBar healthBar;

    private void Start()
    {
        if (interactionText != null) interactionText.SetActive(false);
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (interactionText != null) interactionText.SetActive(true);
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
            Debug.Log(healthBar);
            if (healthBar != null)
            {
                healthBar.IncreaseValue(20f); // Augmente la barre
            }
        }
    }
}