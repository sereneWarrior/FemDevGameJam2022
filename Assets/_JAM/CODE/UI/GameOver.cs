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
        timeText.text = minutes.ToString() + ":" + seconds.ToString();
    }

    private void OnDestroy()
    {
        playerUnit.onUnitDeath -= OnGameOver;
    }
}
