using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Do not check range again if we already check for targets with same or higher range
/// Target check is not optimized. Its really shitty
/// Targeting must be inside the Spell because each spell has different target conditions
/// </summary>

[RequireComponent(typeof(Unit))]
public class PlayerAI : MonoBehaviour
{
    [Header("DESIGN")]
    public List<BaseSpell> spellList = new List<BaseSpell>();
    public LayerMask targetLayer;

    [Header("REFERENCES")]
    private Unit unit;
    public Unit target;

    public void Awake()
    {
        unit = this.GetComponent<Unit>();
        GameManger.playerUnit = unit;
    }

    public void OnEnable()
    {
        foreach (BaseSpell spell in spellList)
        {
            spell.SetupSpell();
        }
    }

    public void Update()
    {
        foreach (BaseSpell spell in spellList)
        {
            if (CheckIfTargetIsValid(spell))
            {
                if (TryToUseSpell(spell))
                    return;
            }
        }
    }

    /// <summary>
    /// We try to attack the Target with a Spell
    /// </summary>
    private bool TryToUseSpell(BaseSpell _spell)
    {
        if (_spell.CheckSpellCooldown())
        {
            _spell.CastSpell(target);
            return true;
        }

        return false;
    }

    /// <summary>
    /// We check if the Target is Valid for this spell
    /// </summary>
    /// <param name="_spell"></param>
    /// <returns></returns>
    private bool CheckIfTargetIsValid(BaseSpell _spell)
    {
        if (target == null)
            TryGetTarget(_spell.modifiedRange);

        if (target == null)
            return false;
        else
        {
            // If our current Target is outside of our Range
            if (Vector3.Distance(target.transform.position, transform.position) > _spell.modifiedRange)
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

        foreach (BaseSpell spell in spellList)
        {
            if(spell != null)
                Gizmos.DrawWireSphere(transform.position, spell.modifiedRange);
        }
    }
}
