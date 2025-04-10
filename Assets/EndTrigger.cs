using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject endScreenImage; // L'image UI à activer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ou tu peux utiliser un autre système d'identification
        {
            if (endScreenImage != null)
            {
                endScreenImage.SetActive(true);
            }

            // Optionnel : bloquer le mouvement du joueur ou faire un effet
            Debug.Log("Fin atteinte !");
        }
    }
}