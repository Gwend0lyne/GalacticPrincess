using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [Header("Collection Settings")]
    public int totalToCollect = 9;
    private int collected = 0;
    private bool hasCollectedOnce = false;
    public StartUIAnimation startUIController;

    [Header("Objects to Activate When Power is Ready")]
    public GameObject[] powerElements;
    
    public UIBanner powerBanner;

    public void CollectItem()
    {
        collected++;
        Debug.Log($"Objet collecté ({collected}/{totalToCollect})");

        if (collected >= totalToCollect)
        {
            ActivatePower();
        }
        
        if (!hasCollectedOnce)
        {
            hasCollectedOnce = true;
            startUIController.HideStartUI();
        }
    }

    private void ActivatePower()
    {
        Debug.Log("💥 Pouvoir activé !");
    
        foreach (GameObject obj in powerElements)
        {
            obj.SetActive(true); // Active l'objet au cas où il était désactivé

            // Si un GravityAreaUp existe dessus, on l'active aussi
            GravityAreaUp gravityUp = obj.GetComponent<GravityAreaUp>();
            if (gravityUp != null)
            {
                gravityUp.enabled = true;
                Debug.Log("✅ Gravité activée sur " + obj.name);
            }
        }

        if (powerBanner != null)
            powerBanner.ShowBanner();
    }
    
    public bool HasPower()
    {
        return collected >= totalToCollect;
    }
    
    public int GetCollectedCount()
    {
        return collected;
    }

}