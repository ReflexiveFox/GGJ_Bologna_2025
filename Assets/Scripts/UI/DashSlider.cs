public class DashSlider : CustomSlider
{
    protected override void Awake()
    {
        base.Awake();
        PlayerController.OnDashTimeChanged += UpdateSliderValue;
        PlayerController.OnDashCooldownChanged += UpdateSliderValue;
    }

    private void OnDestroy()
    {
        PlayerController.OnDashTimeChanged -= UpdateSliderValue;
        PlayerController.OnDashCooldownChanged -= UpdateSliderValue;
    }

    private void UpdateSliderValue(float newValue)
    {
        slider.value = newValue;
    }
}
