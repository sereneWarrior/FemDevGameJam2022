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
            Vector3 input = Vector3.zero;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                input.x = Input.GetAxis("Horizontal");
                input.z = Input.GetAxis("Vertical");
                input.Normalize();
            }
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
