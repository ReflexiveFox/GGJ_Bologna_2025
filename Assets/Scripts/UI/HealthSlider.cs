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

    private void UpdateSlider(int newHealthValue, int maxHealthValue)
    {
        slider.value = (float)newHealthValue / maxHealthValue;
    }
}
