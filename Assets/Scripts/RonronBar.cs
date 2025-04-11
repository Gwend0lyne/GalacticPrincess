using UnityEngine;
using UnityEngine.UI;

public class RonronBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public float fillSpeed = 1f;

    private PlayerCollector playerCollector;
    private float currentValue = 0f;
    private float maxValue = 1f; // sera défini à partir du total à collecter

    void Start()
    {
        playerCollector = FindObjectOfType<PlayerCollector>();
        if (playerCollector != null)
        {
            maxValue = playerCollector.totalToCollect;
            Debug.Log("playerCollector.totalToCollect " + playerCollector.totalToCollect);
        }
        else
        {
            slider.maxValue = 70f;
        }
        slider.value = 0f;
    }

    void Update()
    {
        if (playerCollector != null)
        {
            // On obtient le nombre d'items collectés
            float targetValue = playerCollector.GetCollectedCount();
            targetValue = Mathf.Clamp(targetValue, 0f, maxValue);

            // Si l'élément a été collecté, on incrémente la barre
            if (slider.value < targetValue)
            {
                // On met à jour la barre pour qu'elle augmente en fonction du nombre d'éléments collectés
                slider.value = (targetValue / maxValue) * slider.maxValue;
            }

            Debug.Log($"Target Value: {targetValue}, Max Value: {maxValue}, Slider Value: {slider.value}");
        }
    }

    public float GetValue()
    {
        return slider.value / maxValue;
    }
    
    public void IncreaseValue(float amount)
    {
        currentValue += amount;
        Debug.Log("currentValue " + currentValue);
        Debug.Log("slider " + slider.value);
        slider.value = currentValue;
    }
}