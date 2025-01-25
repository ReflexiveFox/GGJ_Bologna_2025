using System;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static event Action OnRecordUpdated;

    public static RecordManager Instance {  get; private set; }

    private const string minutesRecord = "MinutesRecord";
    private const string SecondsRecord = "SecondsRecord";

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

    private void OnDisable()
    {
        Timer.OnTimerStopped -= TrySaveTime;
    }

    private void TrySaveTime(float newTime)
    {
        TimeSpan newTimeSpan = GetTimeSpan(newTime);
        TrySaveTime(newTimeSpan);
    }

    public void TrySaveTime(TimeSpan newTime)
    {
        if(newTime <= GetTimeRecord())
        {
            return;
        }
        PlayerPrefs.SetInt(minutesRecord, newTime.Minutes);
        PlayerPrefs.SetInt(SecondsRecord, newTime.Seconds);
        OnRecordUpdated?.Invoke();
    }

    public string GetTimeRecordString()
    {
        TimeSpan time = GetTimeRecord();
        return string.Format("{0:00} : {1:00}", time.Minutes, time.Seconds);
    }

    public TimeSpan GetTimeRecord()
    {
        return new(
            0,
            PlayerPrefs.GetInt(minutesRecord, 0),
            PlayerPrefs.GetInt(SecondsRecord, 0)
        );
    }
    private TimeSpan GetTimeSpan(float time)
    {
        return new TimeSpan(0, Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
    }

    public bool IsNewRecord(float newTime)
    {
        TimeSpan currentTimeScore = GetTimeSpan(newTime);
        return currentTimeScore > GetTimeRecord();
    }
}
