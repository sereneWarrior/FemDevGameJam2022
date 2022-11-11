using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
public class PlayerAI : MonoBehaviour
{
    [Header("DESIGN")]
    public float attackCD = 0.5f;
    public float targetRange = 5;
    public LayerMask targetLayer;

    [Header("DATA")]
    public float attackTimer;

    [Header("REFERENCES")]
    public Unit target;

    public void Awake()
    {
        GameManger.playerTrans = this.transform;
        GameManger.playerDamagable = this.GetComponent<IDamagable>();
    }

    public void OnEnable()
    {
        attackTimer = attackCD;
    }

    public void Update()
    {
        if(CheckIfTargetIsValid())
            TryAttackTarget();
    }

    /// <summary>
    /// We try to attack the Target
    /// </summary>
    private void TryAttackTarget()
    {
        if (attackTimer <= 0)
        {
            target.GetDamage(1);
            attackTimer = attackCD;
        }
        else
            attackTimer -= Time.deltaTime;
    }

    /// <summary>
    /// We check if target is valid
    /// </summary>
    /// <returns></returns>
    private bool CheckIfTargetIsValid()
    {
        if (target == null)
            TryGetTarget(targetRange);

        if (target == null)
            return false;
        else
        {
            // If our current Target is outside of our Range
            if (Vector3.Distance(target.transform.position, transform.position) > targetRange)
            {
                RemoveTarget();
                return false;
            }
        }

        // We have a valid Target
        return true;
    }

    /// <summary>
    /// We try to get a new Target
    /// </summary>
    private void TryGetTarget(float _targetRange)
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, _targetRange, targetLayer);
        if(collider.Length > 0)
        {
            target = collider[0].GetComponent<Unit>();
            target.onUnitDisable += RemoveTarget;
        }
    }

    /// <summary>
    /// We Remove the Target
    /// </summary>
    public void RemoveTarget()
    {
        target.onUnitDisable -= RemoveTarget;
        target = null;
    }

    /// <summary>
    /// We get the nearest Target
    /// </summary>
    /// <param name="_collider"></param>
    private void GetNearestTarget(Collider[] _collider)
    {

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
