using UnityEngine;
using UnityEngine.UI;

public class RonronBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public float fillSpeed = 50f;

    private PlayerCollector playerCollector;
    private float currentValue = 0f;
    private float maxValue = 1f; // sera défini à partir du total à collecter

    void Start()
    {
        playerCollector = FindObjectOfType<PlayerCollector>();
        if (playerCollector != null)
        {
            maxValue = playerCollector.totalToCollect;
        }

        slider.maxValue = 70f;
        slider.value = 0f;
    }

    void Update()
    {
        if (playerCollector != null)
        {
            float targetValue = playerCollector.GetCollectedCount(); // Nouveau getter qu'on va créer
            targetValue = Mathf.Clamp(targetValue, 0f, maxValue);

            if (slider.value < targetValue)
            {
                slider.value += fillSpeed * Time.deltaTime;
            }
            else if (slider.value > targetValue)
            {
                slider.value = targetValue; // instant snap si on dépasse
            }

            currentValue = targetValue;
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