using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [Header("DESIGN")]
    [Tooltip("The Offset from the Player position from which we cast the Spells")]
    public Vector3 castPosOffset = new Vector3(0, 1.5f, 0);
    [Tooltip("List that holds all the spells we can currently cast")]
    public List<BaseSpell> spellList = new List<BaseSpell>();

    [Header("REUSABLES")]
    private Vector3 castPos;

    public void OnEnable()
    {
        foreach (BaseSpell spell in spellList)
        {
            spell.SetupSpell();
        }
    }

    public void Update()
    {
        foreach (BaseSpell spell in spellList)
        {
            if (TryToUseSpell(spell))
                return;
        }
    }

    /// <summary>
    /// We try to attack the Target with a Spell
    /// </summary>
    private bool TryToUseSpell(BaseSpell _spell)
    {
        if (_spell.CheckSpellCooldown())
        {
            // We get a target for the Spell
            Unit target = Utilities.GetNearestTargetInRange(transform.position, _spell.modifiedRange, GameManger.enemyLayer);

            // If we did not get a target we return
            if (target == null)
                return false;

            // Else we cast the Spell
            castPos = new Vector3(transform.position.x + castPosOffset.x, transform.position.y + castPosOffset.y, transform.position.z + castPosOffset.z);
            _spell.CastSpell(target, castPos);
            return true;
        }

        return false;
    }
}
