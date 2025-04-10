using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCollector player = other.GetComponent<PlayerCollector>();
        if (player != null)
        {
            player.CollectItem();
            Destroy(gameObject); // Fait disparaître l’objet collecté
            Destroy(transform.parent.gameObject);
        }
    }
}