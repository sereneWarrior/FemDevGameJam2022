using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public static class Utilities
{
    /// <summary>
    /// We get the nearest Target in Range
    /// </summary>
    /// <param name="_unit"></param>
    /// <param name="_range"></param>
    /// <param name="_layerMask"></param>
    /// <returns></returns>
    public static Unit GetNearestTargetInRange(Vector3 _startPos, float _range, LayerMask _layerMask)
    {
        // We create the Container we neeed
        float distance = Mathf.Infinity;
        Unit nearestUnit = null;

        // We get the Collider
        Collider[] collider = Physics.OverlapSphere(_startPos, _range, _layerMask);

        // We go trough each collider and get the closest
        foreach (Collider enemy in collider)
        {
            float tempDistance = Vector3.Distance(_startPos, enemy.transform.position);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                nearestUnit = enemy.GetComponent<Unit>();
            }
        }

        // We return the nearest Unit
        return nearestUnit;
    }
}
