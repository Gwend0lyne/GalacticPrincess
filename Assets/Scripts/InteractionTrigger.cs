using System;
using UnityEngine;
using TMPro; 

public class InteractionTrigger : MonoBehaviour
{
    public event Action OnInteract;
    private bool isPlayerInside = false;
    [SerializeField] private GameObject interactionText; 

    private void Start()
    {
        if (interactionText != null) interactionText.SetActive(false);
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
        }
    }
}