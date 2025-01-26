using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] protected AudioMixerGroup audioMixerGroup;
    [SerializeField] protected string volumeToSet;
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        audioMixerGroup.audioMixer.GetFloat(volumeToSet, out float volume);
        volumeSlider.value = GetSliderValue(volume);
    }

    protected float GetConvertedVolume(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }

    protected float GetSliderValue(float convertedVolume)
    {
        return Mathf.Pow(10, convertedVolume / 20);
    }


    public void SetVolume(float sliderValue)
    {
        audioMixerGroup.audioMixer.SetFloat(volumeToSet, GetConvertedVolume(sliderValue));
    }
}
