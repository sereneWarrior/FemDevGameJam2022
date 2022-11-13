using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunTimer : MonoBehaviour, IPausable
{
    [Header("REFERENCES")]
    public TMP_Text timerDisplay;
    [HideInInspector]
    public float timer;
    private bool isPaused;

    public static RunTimer instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        timer = 0;
        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    private void OnDisable()
    {
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame -= UnpauseCode;
    }

    public void Update()
    {
        if (isPaused)
            return;

        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);
        string minutesString = minutes.ToString();
        string secondsString = seconds.ToString();
        if (minutes < 10)
        {
            minutesString = "0" + minutes.ToString();
        }
        if (seconds < 10)
        {
            secondsString = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        timerDisplay.text = minutesString + ":" + secondsString;
    }

    public void PauseCode()
    {
        isPaused = true;
    }

    public void UnpauseCode()
    {
        isPaused = false;
    }
}
