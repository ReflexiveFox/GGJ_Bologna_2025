using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static event Action<float> OnTimerStopped;

    [SerializeField] private TextMeshProUGUI timerResultText;
    private TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool canCount = true;

    private void Awake()
    {
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        PlayerHealth.OnPlayerDied += StopCounting;
    }

    private void Start()
    {
        elapsedTime = 0;
    }

    private void Update()
    {
        if (!canCount) return;

        elapsedTime += Time.deltaTime;
        timerText.text = GetTimeString();
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= StopCounting;
    }

    private void StopCounting()
    {
        canCount = false;
        timerResultText.text = GetTimeString();
        OnTimerStopped?.Invoke(elapsedTime);
    }

    private string GetTimeString()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
