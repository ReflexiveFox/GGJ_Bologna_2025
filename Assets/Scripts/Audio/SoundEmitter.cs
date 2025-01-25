using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine playingCoroutine;
    public SoundData Data { get; private set; }
    private void Awake()
    {
        audioSource = gameObject.GetOrAddComponent<AudioSource>();
    }

    public void Play()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }

        audioSource.Play();
        playingCoroutine = StartCoroutine(WaitForSoundToEnd());
    }

    private IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        SoundManager.Instance.ReturnToPool(this);
    }

    public void Stop()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }

        audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }

    public void Initialize(SoundData soundData)
    {
        Data = soundData;
        audioSource.clip = soundData.clip;
        audioSource.outputAudioMixerGroup = soundData.mixerGroup;
        audioSource.loop = soundData.loop;
        audioSource.playOnAwake = soundData.playOnAwake;
    }

    public void WithRandomPitch(float min = -.05f, float max = .05f)
    {
        audioSource.pitch += Random.Range(min, max);
    }
}
