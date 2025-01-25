using UnityEngine;

public class CanvasScreen : MonoBehaviour
{
    private Canvas canvas;

    protected virtual void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }
}
