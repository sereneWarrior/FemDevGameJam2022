using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("DESIGN")]
    public float targetReachedTreshold = 0.2f;

    [Header("DATA")]
    private float damage;
    private float speed;

    [Header("REFERENCES")]
    private Unit targetUnit;

    /// <summary>
    /// We setup this Projectile
    /// </summary>
    /// <param name="_unit"></param>
    public void SetupProjectile(Unit _unit, float _damage, float _speed)
    {
        targetUnit = _unit;
        damage = _damage;
        speed = _speed;
        targetUnit.onUnitDisable += DisableProjectile;
    }

    private void Update()
    {
        MoveProjectile();
        CheckDistance();
    }

    /// <summary>
    /// We move the Projectile
    /// </summary>
    private void MoveProjectile()
    {
        transform.LookAt(targetUnit.transform);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    /// <summary>
    /// We check the Distance between us and the Target
    /// </summary>
    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, targetUnit.transform.position) <= targetReachedTreshold)
        {
            targetUnit.GetDamage(damage);
            DisableProjectile();
        }
    }

    /// <summary>
    /// We disable this Projectile
    /// </summary>
    private void DisableProjectile()
    {
        targetUnit.onUnitDisable -= DisableProjectile;
        Destroy(this.gameObject); // Later we need to add pooling
    }
}
