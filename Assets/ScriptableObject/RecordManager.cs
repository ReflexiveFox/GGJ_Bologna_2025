using System;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static event Action OnRecordUpdated;

    public static RecordManager Instance {  get; private set; }

    private const string minutesRecord = "MinutesRecord";
    private const string secondsRecord = "secondsRecord";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Timer.OnTimerStopped += TrySaveTime;
    }

    private void Start()
    {
        if(!(PlayerPrefs.HasKey(minutesRecord) && PlayerPrefs.HasKey(secondsRecord)))
        {
            TrySaveTime(new TimeSpan(0, 0, 0));
        }
    }

    private void OnDisable()
    {
        Timer.OnTimerStopped -= TrySaveTime;
    }

    private void TrySaveTime(float newTime)
    {
        TimeSpan newTimeSpan = GetTimeSpan(newTime);
        TrySaveTime(newTimeSpan);
    }

    private void TrySaveTime(TimeSpan newTime)
    {
        if(newTime <= GetTimeRecord())
        {
            return;
        }
        PlayerPrefs.SetInt(minutesRecord, newTime.Minutes);
        PlayerPrefs.SetInt(secondsRecord, newTime.Seconds);
        OnRecordUpdated?.Invoke();
    }

    private TimeSpan GetTimeRecord()
    {
        return new(
            0,
            PlayerPrefs.GetInt(minutesRecord, 0),
            PlayerPrefs.GetInt(secondsRecord, 0)
        );
    }
    private TimeSpan GetTimeSpan(float time)
    {
        return new TimeSpan(0, Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
    }


    public string GetTimeRecordString()
    {
        TimeSpan time = GetTimeRecord();
        Debug.Log($"{nameof(time.Minutes)}: {time.Minutes}, {nameof(time.Seconds)}: {time.Seconds}");
        return string.Format("{0:00} : {1:00}", time.Minutes, time.Seconds);
    }
}
