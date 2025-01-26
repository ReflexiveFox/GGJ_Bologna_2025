using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private IObjectPool<SoundEmitter> soundEmitterPool;

    private readonly List<SoundEmitter> activeSoundEmitters = new();

    public readonly Queue<SoundEmitter> FrequentSoundEmitters = new();
    [SerializeField] private SoundData menuBackground;
    [SerializeField] private SoundData gameBackground;
    [SerializeField] private SoundEmitter soundEmitterPrefab;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private int maxSoundInstances = 30;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += ApplyBackgroundMusic;
        }
        else
        {
            Destroy(gameObject);
            return;
        }   
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ApplyBackgroundMusic;
    }

    private void ApplyBackgroundMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0)
        {
            CreateSound().WithSoundData(menuBackground);
        }
        else
        {
            CreateSound().WithSoundData(gameBackground);

            InitializePool();
        }
    }

    public SoundBuilder CreateSound() => new SoundBuilder(this);

    public bool CanPlaySound(SoundData soundData)
    { 
        if (!soundData.frequentSound)
        {
            return true;
        }

        if(FrequentSoundEmitters.Count >= maxSoundInstances && FrequentSoundEmitters.TryDequeue(out SoundEmitter soundEmitter))
        {
            try
            {
                soundEmitter.Stop();
                return true;
            }
            catch
            {
                Debug.Log("SoundEmitter is already released");
            }
            return false;
        }
        return true;

    }

    public SoundEmitter Get()
    {
        return soundEmitterPool.Get();
    }

    public void ReturnToPool(SoundEmitter soundEmitter)
    {
        soundEmitterPool.Release(soundEmitter);
    }

    private void OnDestroyPoolObject(SoundEmitter soundEmitter)
    {
        Destroy(soundEmitter.gameObject);
    }

    private void OnReturnedToPool(SoundEmitter soundEmitter)
    {
        soundEmitter.gameObject.SetActive(false);
        activeSoundEmitters.Remove(soundEmitter);
    }

    private void OnTakeFromPool(SoundEmitter soundEmitter)
    {
        soundEmitter.gameObject.SetActive(true);
        activeSoundEmitters.Add(soundEmitter);
    }

    private SoundEmitter CreateSoundEmitter()
    {
        SoundEmitter soundEmitter = Instantiate(soundEmitterPrefab);
        soundEmitter.gameObject.SetActive(false);
        return soundEmitter;
    }

    private void InitializePool()
    {
        soundEmitterPool = new ObjectPool<SoundEmitter>(
            CreateSoundEmitter, 
            OnTakeFromPool, 
            OnReturnedToPool, 
            OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxPoolSize);
    }
}