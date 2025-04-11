using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject lapsGroup; // <- référence directe au composant Image
    private Image lapBackground; 
    
    void Start()
    {
        lapBackground = lapsGroup.GetComponent<Image>();
        Debug.Log("laps " + lapBackground);
        Debug.Log("laps " + lapsGroup);

        // Cache les éléments au départ
        if (lapBackground != null)
        {
            lapBackground.enabled = false; // Caché au début
        }

        if (text != null)
        {
            text.enabled = false; // Caché au début
        }
    }
    
    public void UpdateLapText(string message)
    {
        bool hasText = !string.IsNullOrEmpty(message);
        
        text.text = message;

        if (lapBackground != null)
        {
            lapBackground.enabled = hasText;
        }
        text.enabled = hasText;
    }
}