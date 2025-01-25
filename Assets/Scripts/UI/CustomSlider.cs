using UnityEngine;
using UnityEngine.UI;

public abstract class CustomSlider : MonoBehaviour
{
    protected Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }
}

public class HealthSlider : CustomSlider
{
    protected override void Awake()
    {
        base.Awake();
        PlayerHealth.OnHealthChanged += UpdateSlider;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthChanged -= UpdateSlider;
    }

    private void UpdateSlider(int newHealthValue)
    {
        slider.value = newHealthValue;
    }
}
