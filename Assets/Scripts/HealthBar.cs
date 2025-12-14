using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image fillImage;

    void Start()
    {
        if (fillImage == null) Debug.LogError("Fill Image n'est pas assigné!");
        if (healthGradient == null) Debug.LogError("Health Gradient n'est pas assigné!");
        if (healthSlider == null) Debug.LogError("Health Slider n'est pas assigné!");
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(1f);
        }
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        float normalizedValue = healthSlider.normalizedValue;
        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(normalizedValue);
        }
    }
}