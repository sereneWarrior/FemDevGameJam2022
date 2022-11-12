using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "Gamejam/Create new ProjectileSpell", order = 0)]
public class ProjectileSpell : BaseSpell
{
    [Header("PROJECTILE SPELL")]
    [SerializeField] public float projectileSpeed = 5;
    [HideInInspector] public float modifiedSpeed;
    public Projectile projectilePrefab;

    /// <summary>
    /// We Setup this Spell
    /// </summary>
    public override void SetupSpell()
    {
        base.SetupSpell();
        modifiedSpeed = projectileSpeed;

        if (projectilePrefab == null)
            Debug.LogError("ProjectileSpell: We did not reference the Projectile!");
    }

    /// <summary>
    /// We cast this spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    public override void CastSpell(Unit _target, Vector3 _castPos)
    {
        Instantiate(projectilePrefab, _castPos, Quaternion.identity).SetupProjectile(_target, modifiedDamage, modifiedSpeed, modifiedSplashRadius);
        base.CastSpell(_target, _castPos);
    }
}
