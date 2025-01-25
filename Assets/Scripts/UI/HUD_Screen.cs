public class HUD_Screen : CanvasScreen
{
    protected override void Awake()
    {
        base.Awake();
        PlayerHealth.OnPlayerDied += Hide;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= Hide;
    }

    private void Start()
    {
        Show();
    }
}
