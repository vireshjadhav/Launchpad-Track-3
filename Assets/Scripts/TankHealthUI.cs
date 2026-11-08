using UnityEngine;
using UnityEngine.UI;

public class TankHealthUI : MonoBehaviour
{
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color zeroHealthColor = Color.red; 

    [SerializeField] private Image fillImage;
    [SerializeField] private Slider healthSlider;

    public void UpdateHealth(float currentHealt, float maxHealth)
    {
        float healthPercentage = Mathf.Clamp01(currentHealt / maxHealth);
        healthSlider.value = healthPercentage;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercentage);
    }
}
