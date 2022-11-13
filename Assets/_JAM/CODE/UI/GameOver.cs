using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Unit playerUnit;

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
    }

    private void OnDestroy()
    {
        playerUnit.onUnitDeath -= OnGameOver;
    }
}
