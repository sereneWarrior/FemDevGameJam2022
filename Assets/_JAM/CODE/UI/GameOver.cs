using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public Unit playerUnit;
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        playerUnit.onUnitDeath += OnGameOver;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        GameManger.SendPauseGameEvent();
        gameObject.SetActive(true);
        float timer = RunTimer.instance.timer;
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
        timeText.text = minutes.ToString() + ":" + seconds.ToString();
    }

    private void OnDestroy()
    {
        playerUnit.onUnitDeath -= OnGameOver;
    }
}
