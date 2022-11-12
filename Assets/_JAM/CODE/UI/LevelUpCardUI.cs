using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpCardUI : MonoBehaviour
{
    [Header("UI REFERENCES")]
    public TMP_Text nameDisplay;
    public TMP_Text levelDisplay;
    public TMP_Text damageDisplay;
    public TMP_Text cooldownDisplay;
    public TMP_Text rangeDisplay;
    public TMP_Text splashDisplay;

    [Header("REFERENCES")]
    private SpellCaster caster;

    /// <summary>
    /// We Setup this Card
    /// </summary>
    /// <param name="_containerID"></param>
    /// <param name="_caster"></param>
    public void SetupCard(int _containerID, SpellCaster _caster)
    {
        // We get the Data we need
        int spellLevel = _caster.spellList[_containerID].level;
        BaseSpell spellReference = _caster.spellList[_containerID].spellReference;
        caster = _caster;

        // We set the base display Data
        nameDisplay.text = spellReference.name;
        levelDisplay.text = spellLevel.ToString();
        damageDisplay.text = spellReference.leveledSpellStats[spellLevel].damage.ToString();
        cooldownDisplay.text = spellReference.leveledSpellStats[spellLevel].cooldown.ToString();
        rangeDisplay.text = spellReference.leveledSpellStats[spellLevel].range.ToString();
        splashDisplay.text = spellReference.leveledSpellStats[spellLevel].splashRadius.ToString();

        // If we have not learned this skill yet we return
        if (!_caster.spellList[_containerID].learned)
            return;

        // We display the Upgrade Stats
        levelDisplay.text = spellLevel + " => <color=#1C8E40>" + spellLevel + 1;
        damageDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].damage;
        cooldownDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].cooldown;
        rangeDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].range;
        splashDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].splashRadius;
    }

    /// <summary>
    /// We Toggle the Collider of this Card
    /// </summary>
    /// <param name="_isActive"></param>
    public void ToggleCollider(bool _isActive)
    {

    }
}
