using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
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

    /// <summary>
    /// We damage all in Range
    /// </summary>
    /// <param name="_startPos"></param>
    /// <param name="_radius"></param>
    /// <param name="_damage"></param>
    /// <param name="_layerMask"></param>
    public static void DamageAllInRange(Vector3 _startPos, float _radius, float _damage, LayerMask _layerMask)
    {
        // We get the Collider
        Collider[] collider = Physics.OverlapSphere(_startPos, _radius, _layerMask);

        // We go trough each collider and get the closest
        foreach (Collider enemy in collider)
        {
            if(enemy != null && enemy.gameObject.activeSelf)
                enemy.GetComponent<IDamagable>().GetDamage(_damage);
        }
    }
}
