using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color warningColor = Color.red;

    private float maxHealth = 100f;

    public void SetMaxHealth(float max)
    {
        maxHealth = max;
        healthSlider.maxValue = max;
        healthSlider.value = max;
        UpdateColor(max);
    }

    public void SetHealth(float current)
    {
        healthSlider.value = Mathf.Clamp(current, 0, maxHealth);
        UpdateColor(current);
    }

    private void UpdateColor(float current)
    {
        if (fillImage != null)
        {
            fillImage.color = (current <= maxHealth * 0.3f) ? warningColor : safeColor;
        }
    }
}
