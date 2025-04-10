using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PowerWall : MonoBehaviour
{
    public UIBanner noPowerBanner;
    private void OnTriggerEnter(Collider other)
    {
        PlayerCollector player = other.GetComponent<PlayerCollector>();
        if (player != null)
        {
            if (!player.HasPower())
            {
                Debug.Log("NON tu peux pas sauter !");
                if (noPowerBanner != null)
                    noPowerBanner.ShowBanner();
            }
        }
    }
}