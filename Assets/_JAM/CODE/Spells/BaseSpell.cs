using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : ScriptableObject
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
    [Tooltip("If we hit other enemies beside the Target in range")]
    [SerializeField] private float splashRadius = 0;
    [HideInInspector] public float modifiedSplashRadius;

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
        modifiedSplashRadius = splashRadius;
    }

    /// <summary>
    /// We cast this spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    public virtual void CastSpell(Unit _target, Vector3 _castPos)
    {
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


