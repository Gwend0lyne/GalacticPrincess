using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public float maxValue = 100f;
    private float currentValue = 0f;

    public float fillSpeed = 50f; 

    void Start()
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void IncreaseValue(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue); 
    }

    void Update()
    {
        if (slider.value < currentValue)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

    public float GetValue()
    {
        return slider.value / maxValue; 
    }
}