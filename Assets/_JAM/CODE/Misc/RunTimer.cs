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
        timerDisplay.text = "Time: " + Mathf.FloorToInt(timer);
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
