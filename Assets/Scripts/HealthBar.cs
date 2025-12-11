using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
 
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image fillImage;

    void Start()
    {
        // Vérification au démarrage
        if (fillImage == null)
        {
            Debug.LogError("Fill Image n'est pas assigné!");
        }
        if (healthGradient == null)
        {
            Debug.LogError("Health Gradient n'est pas assigné!");
        }
        if (healthSlider == null)
        {
            Debug.LogError("Health Slider n'est pas assigné!");
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(1f);
            Debug.Log($"SetMaxHealth - Color set to: {fillImage.color}");
        }
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        
        float normalizedValue = healthSlider.normalizedValue;
        Color newColor = healthGradient.Evaluate(normalizedValue);
        
        if (fillImage != null)
        {
            fillImage.color = newColor;
            Debug.Log($"SetHealth - Health: {health}/{healthSlider.maxValue}, Normalized: {normalizedValue:F2}, Color: {newColor}");
        }
        else
        {
            Debug.LogError("fillImage est null dans SetHealth!");
        }
    }
}
