public class GameOverScreen : CanvasScreen 
{
    protected override void Awake()
    {
        base.Awake();
        PlayerHealth.OnPlayerDied += Show;
    }

    private void Start()
    {
        Hide();
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= Show;
    }
}
