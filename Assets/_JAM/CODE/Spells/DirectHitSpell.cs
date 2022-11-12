using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectHitSpell", menuName = "Gamejam/Create new DirectHitSpell", order = 0)]
public class DirectHitSpell : BaseSpell
{
    /// <summary>
    /// We cast this Spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    public override void CastSpell(Unit _target, Vector3 _castPos)
    {
        if (modifiedSplashRadius == 0)
            _target.GetDamage(modifiedDamage);
        else
            Utilities.DamageAllInRange(_target.transform.position, modifiedSplashRadius, modifiedDamage, GameManger.enemyLayer);

        base.CastSpell(_target, _castPos);
    }
}
