using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "Gamejam/Create new ProjectileSpell", order = 0)]
public class ProjectileSpell : BaseSpell
{
    [Header("PROJECTILE SPELL")]
    [Tooltip("Speed of the Projectile we shoot")]
    public float projectileSpeed = 5;
    public SpellProjectile projectilePrefab;

    /// <summary>
    /// We Setup this Spell
    /// </summary>
    public void SetupSpell()
    {
        if (projectilePrefab == null)
            Debug.LogError("ProjectileSpell: We did not reference the Projectile!");
    }

    /// <summary>
    /// We cast this spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    /// <param name="_spellLevel"></param>
    public override void CastSpell(Unit _target, Vector3 _castPos, int _spellLevel)
    {
        Instantiate(projectilePrefab, _castPos, Quaternion.identity).SetupProjectile(_target, leveledSpellStats[_spellLevel].damage, projectileSpeed, leveledSpellStats[_spellLevel].splashRadius);
        base.CastSpell(_target, _castPos, _spellLevel);
    }
}
