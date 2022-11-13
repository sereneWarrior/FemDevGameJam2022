using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeathEffect : MonoBehaviour
{
    [Header("REFERENCES")]
    public Unit unit;
    public GameObject deathVFX;

    public void OnEnable()
    {
        unit.onUnitDisable += SpawnDeathEffect;
    }

    public void OnDisable()
    {
        unit.onUnitDisable -= SpawnDeathEffect;
    }

    /// <summary>
    /// We spawn a Death Effect
    /// </summary>
    private void SpawnDeathEffect()
    {
        Instantiate(deathVFX, unit.transform.position, Quaternion.identity);
    }
}
