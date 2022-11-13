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
    /// <param name="_castPos"></param>
    /// <param name="_spellLevel"></param>
    public override void CastSpell(Vector3 _castPos, int _spellLevel)
    {
        // We create the container
        Unit tempTarget;
        Vector3 castPos = _castPos;

        // We try to get a new Target
        tempTarget = null;
        tempTarget = Utilities.GetEnemiesInRange(castPos, leveledSpellStats[_spellLevel].range);

        // If we got no target we return
        if (tempTarget == null)
            return;

        Instantiate(projectilePrefab, _castPos, Quaternion.identity).SetupProjectile(tempTarget, projectileSpeed, leveledSpellStats[_spellLevel]);
        base.CastSpell(_castPos, _spellLevel);
    }
}
