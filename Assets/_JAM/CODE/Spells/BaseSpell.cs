using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : ScriptableObject
{
    [Header("DESIGN")]
    [Tooltip("The name of the Spell")]
    public string spellName = "BaseSpell";
    [Tooltip("Holds the Stats for each Spell Level")]
    public List<SpellStats> leveledSpellStats = new List<SpellStats>();

    [Header("DATA")]
    private float spellCDTimer;

    /// <summary>
    /// We cast this spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    /// <param name="_spellLevel"></param>
    public virtual void CastSpell(Unit _target, Vector3 _castPos, int _spellLevel)
    {
        spellCDTimer = leveledSpellStats[_spellLevel].cooldown;
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

[System.Serializable]
public class SpellStats
{
    [Tooltip("How much Damage this spell does")]
    public float damage = 1;
    [Tooltip("How long until we can use this spell again")]
    public float cooldown = 0.25f;
    [Tooltip("From how far away we can hit the Enemy")]
    public float range = 5;
    [Tooltip("If we hit other enemies beside the Target in range")]
    public float splashRadius = 0;
}


