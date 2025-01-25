﻿using UnityEngine;

public class SoundBuilder 
{
    private readonly SoundManager soundManager;
    private SoundData soundData;
    private Vector3 position = Vector3.zero;
    private bool randomPitch;

    public SoundBuilder(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public SoundBuilder WithSoundData(SoundData soundData)
    {
        this.soundData = soundData;
        return this;
    }

    public SoundBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomPitch()
    {
        this.randomPitch = true;
        return this;
    }

    public void Play()
    {
        if (!soundManager.CanPlaySound(soundData)) return;

        SoundEmitter soundEmitter = soundManager.Get();
        soundEmitter.Initialize(soundData);
        soundEmitter.transform.position = position;
        soundEmitter.transform.parent = SoundManager.Instance.transform;

        if (randomPitch)
        {
            soundEmitter.WithRandomPitch();
        }

        //soundManager.Counts[soundData] = soundManager.Counts.TryGetValue(soundData, out int count) ? count + 1 : 1;
        if(soundData.frequentSound)
        {
            soundManager.FrequentSoundEmitters.Enqueue(soundEmitter);
        }
        soundEmitter.Play();
    }
}
