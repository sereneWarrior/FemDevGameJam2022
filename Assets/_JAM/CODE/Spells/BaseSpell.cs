using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Gamejam/Create new Spell", order = 0)]
public class BaseSpell : ScriptableObject
{
    [Header("DESIGN")]
    [Tooltip("The name of the Spell")]
    public string spellName = "BaseSpell";
    [Tooltip("How much Damage this spell does")]
    [SerializeField] private float damage = 1;
    [HideInInspector] public float modifiedDamage;
    [Tooltip("How long until we can use this spell again")]
    [SerializeField] private float cooldown = 0.25f;
    [HideInInspector] public float modifiedCD;
    [Tooltip("From how far away we can hit the Enemy")]
    [SerializeField] private float range = 5;
    [HideInInspector] public float modifiedRange;

    [Header("DATA")]
    private float spellCDTimer;

    /// <summary>
    /// We Setup this Spell
    /// </summary>
    public virtual void SetupSpell()
    {
        modifiedDamage = damage;
        modifiedCD = cooldown;
        spellCDTimer = modifiedCD;
        modifiedRange = range;
    }

    /// <summary>
    /// We execute this Spell
    /// </summary>
    /// <param name="_target"></param>
    public virtual void CastSpell(Unit _target)
    {
        _target.GetDamage(modifiedDamage);
        spellCDTimer = modifiedCD;
    }

    /// <summary>
    /// We check the Spell cooldown
    /// </summary>
    /// <returns></returns>
    public bool CheckSpellCooldown()
    {
        if (spellCDTimer <= 0)
            return true;
        else
        {
            spellCDTimer -= Time.deltaTime;
            return false;
        }
    }
}
