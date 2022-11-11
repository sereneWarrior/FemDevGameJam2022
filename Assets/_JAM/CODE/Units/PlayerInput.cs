using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> onPlayerInput;

    bool canReadInput;

    private void Awake()
    {
        canReadInput = true;
    }

    private void FixedUpdate()
    {
        if (canReadInput)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input.Normalize();
            if (onPlayerInput != null)
                onPlayerInput(input);
        }
    }
}
