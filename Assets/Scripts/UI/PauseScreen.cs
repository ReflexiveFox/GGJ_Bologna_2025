public class PauseScreen : CanvasScreen
{
    private void Start()
    {
        Hide();
        PauseHandler.OnPause += Show;
        PauseHandler.OnResume += Hide;
    }

    private void OnDestroy()
    {
        PauseHandler.OnPause -= Show;
        PauseHandler.OnResume -= Hide;
    }
}
