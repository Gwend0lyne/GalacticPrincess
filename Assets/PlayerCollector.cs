using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [Header("Collection Settings")]
    public int totalToCollect = 9;
    private int collected = 0;

    [Header("Objects to Activate When Power is Ready")]
    public GameObject[] powerElements;

    public void CollectItem()
    {
        collected++;
        Debug.Log($"Objet collecté ({collected}/{totalToCollect})");

        if (collected >= totalToCollect)
        {
            ActivatePower();
        }
    }

    private void ActivatePower()
    {
        Debug.Log("💥 Pouvoir activé !");
        foreach (GameObject obj in powerElements)
        {
            obj.SetActive(true);
        }
    }
}