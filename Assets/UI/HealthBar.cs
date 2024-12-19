using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI number;

    private void Start()
    {
        gameObject.transform.rotation = new Quaternion(0.298836231f, 0.640856445f, -0.298836231f, 0.640856445f);
    }

    public void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
        number.text = Health.ToString();
    }

    public void UpdateHealth(int Health)
    {
        slider.value = Health;
        number.text = Health.ToString();
    }
}
