using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> onPlayerInput;
    public static Action onEscapePressed;

    bool canReadInput;

    private void Awake()
    {
        canReadInput = true;
        GeneralSettings.onOptionOpen += DisableInput;
    }

    private void LateUpdate()
    {
        if (canReadInput)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input.Normalize();
            if (onPlayerInput != null)
                onPlayerInput(input);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (onEscapePressed != null)
                onEscapePressed();
        }
    }

    void DisableInput(bool isOpen)
    {
        canReadInput = !isOpen;
    }

    private void OnDestroy()
    {
        GeneralSettings.onOptionOpen -= DisableInput;
    }
}
