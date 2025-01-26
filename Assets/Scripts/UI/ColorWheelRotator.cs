using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorWheelRotator : MonoBehaviour
{
    private Image targetImage;

    [SerializeField, Tooltip("Saturation of the color (fixed).")]
    [Range(0f, 1f)] private float saturation = .8f;

    [SerializeField, Tooltip("Brightness of the color (fixed).")]
    [Range(0f,1f)] private float brightness = .8f;

    [SerializeField, Tooltip("Speed of hue rotation.")]
    private float rotationSpeed = 1f;

    private float hue = 0f; // Starting hue

    private void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    private void Update()
    {
        hue += rotationSpeed * Time.deltaTime;
        hue %= 1f;

        targetImage.color = Color.HSVToRGB(hue, saturation, brightness);
    }
}

