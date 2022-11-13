using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionSpell", menuName = "Gamejam/Create new ExplosionSpell", order = 0)]
public class ExplosionSpell : BaseSpell
{
    [Header("ExplosionSpell")]
    public float yPos;
    public GameObject explosionVFX;

    public override void CastSpell(Vector3 _castPos, int _spellLevel)
    {
        Utilities.DamageAllInRange(_castPos, leveledSpellStats[_spellLevel].splashRadius, leveledSpellStats[_spellLevel].damage, GameManger.enemyLayer);

        if (explosionVFX == null)
            return;

        Vector3 splashPos = new Vector3(_castPos.x, yPos, _castPos.z);
        Instantiate(explosionVFX, splashPos, Quaternion.identity);

        base.CastSpell(_castPos, _spellLevel);
    }
}
