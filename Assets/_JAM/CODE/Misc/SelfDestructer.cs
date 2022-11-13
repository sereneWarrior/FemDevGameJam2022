using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructer : MonoBehaviour, IPausable
{
    [Header("DESIGN")]
    public float selfDestructTime = 1;
    private float selfDestructTimer;

    [Header("DATA")]
    private bool isPaused = false;

    private void OnEnable()
    {
        selfDestructTimer = selfDestructTime;
        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    private void OnDisable()
    {
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame -= UnpauseCode;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
            return;

        if (selfDestructTimer <= 0)
            Destroy(gameObject);
        else
            selfDestructTimer -= Time.deltaTime;
    }

    /// <summary>
    /// We Pause the Code
    /// </summary>
    public void PauseCode()
    {
        isPaused = true;
    }

    /// <summary>
    /// We unpause the Code
    /// </summary>
    public void UnpauseCode()
    {
        isPaused = false;
    }
}
