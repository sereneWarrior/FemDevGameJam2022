using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectHitSpell", menuName = "Gamejam/Create new DirectHitSpell", order = 0)]
public class DirectHitSpell : BaseSpell
{
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

        for (int i = 0; i < leveledSpellStats[_spellLevel].bounce + 1; i++)
        {
            // We try to get a new Target
            tempTarget = null;
            tempTarget = Utilities.GetEnemiesInRange(castPos, leveledSpellStats[_spellLevel].range);

            // If we got no target we return
            if (tempTarget == null)
                return;

            // We actually cast the Spell
            if (leveledSpellStats[_spellLevel].splashRadius == 0)
                tempTarget.GetDamage(leveledSpellStats[_spellLevel].damage);
            else
                Utilities.DamageAllInRange(tempTarget.transform.position, leveledSpellStats[_spellLevel].splashRadius, leveledSpellStats[_spellLevel].damage, GameManger.enemyLayer);

            // We move the Cast Pos
            castPos = tempTarget.transform.position;
        }

        base.CastSpell(_castPos, _spellLevel);
    }
}
