using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("REFERENCES")]
    public Unit unit;

    public void Update()
    {
        GetPlayerInput();
    }

    /// <summary>
    /// We get the Player Input
    /// </summary>
    public void GetPlayerInput()
    {

    }

    /// <summary>
    /// We get the Mouse Worldpos
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMouseWorldPos()
    {
        return Vector3.zero;
    }
}
