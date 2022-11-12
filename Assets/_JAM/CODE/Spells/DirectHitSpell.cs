using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectHitSpell", menuName = "Gamejam/Create new DirectHitSpell", order = 0)]
public class DirectHitSpell : BaseSpell
{
    /// <summary>
    /// We cast this spell
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_castPos"></param>
    /// <param name="_spellLevel"></param>
    public override void CastSpell(Unit _target, Vector3 _castPos, int _spellLevel)
    {
        if (leveledSpellStats[_spellLevel].splashRadius == 0)
            _target.GetDamage(leveledSpellStats[_spellLevel].damage);
        else
            Utilities.DamageAllInRange(_target.transform.position, leveledSpellStats[_spellLevel].splashRadius, leveledSpellStats[_spellLevel].damage, GameManger.enemyLayer);

        base.CastSpell(_target, _castPos, _spellLevel);
    }
}
