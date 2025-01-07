using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public TextMeshProUGUI number;

    private void Start()
    {
        gameObject.transform.rotation = new Quaternion(0.298836231f, 0.640856445f, -0.298836231f, 0.640856445f);
    }

    public void SetMaxHealth(int Health)
    {
        slider1.maxValue = Health;
        slider2.maxValue = Health;
        slider1.value = Health;
        slider2.value = Health;
        number.text = Health.ToString();
    }

    public void UpdateHealth(int Health)
    {
        slider1.value = Health;
        number.text = Health.ToString();
        StartCoroutine(UpdateSlider2(Health));
    }

    IEnumerator UpdateSlider2(int Health)
    {
        yield return new WaitForSeconds(1f);
        slider2.value = Health;
    }
}
