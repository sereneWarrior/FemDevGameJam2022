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
    private float splashRadius;

    [Header("REFERENCES")]
    private Unit targetUnit;

    /// <summary>
    /// We Setup the Projectile
    /// </summary>
    /// <param name="_unit"></param>
    /// <param name="_damage"></param>
    /// <param name="_speed"></param>
    /// <param name="_splashRadius"></param>
    public void SetupProjectile(Unit _unit, float _damage, float _speed, float _splashRadius = 0)
    {
        targetUnit = _unit;
        damage = _damage;
        speed = _speed;
        splashRadius = _splashRadius;
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
            MakeDamage();
            DisableProjectile();
        }
    }

    /// <summary>
    /// We damage the Target
    /// </summary>
    public void MakeDamage()
    {
        if (splashRadius == 0)
            targetUnit.GetDamage(damage);
        else
            Utilities.DamageAllInRange(targetUnit.transform.position, splashRadius, damage, GameManger.enemyLayer);
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
