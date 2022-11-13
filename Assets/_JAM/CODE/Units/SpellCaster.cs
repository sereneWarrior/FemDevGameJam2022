using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, IPausable
{
    [Header("DESIGN")]
    [Tooltip("The Offset from the Player position from which we cast the Spells")]
    public Vector3 castPosOffset = new Vector3(0, 1.5f, 0);
    [Tooltip("List that holds all the spells we can currently cast")]
    public List<SpellContainer> spellList = new List<SpellContainer>();

    [Header("DATA")]
    public bool isPaused = false;

    [Header("REUSABLES")]
    private Vector3 castPos;

    public void OnEnable()
    {
        foreach (SpellContainer spell in spellList)
        {
            spell.level = 0;
        }

        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    public void OnDisable()
    {
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame -= UnpauseCode;
    }

    public void Update()
    {
        if (isPaused)
            return;

        foreach (SpellContainer spell in spellList)
        {
            if(spell.learned)
                if (TryToUseSpell(spell.spellReference, spell.level))
                    return;
        }
    }

    /// <summary>
    /// We try to attack the Target with a Spell
    /// </summary>
    private bool TryToUseSpell(BaseSpell _spell, int _spellLevel)
    {
        if (_spell.CheckSpellCooldown())
        {
            castPos = new Vector3(transform.position.x + castPosOffset.x, transform.position.y + castPosOffset.y, transform.position.z + castPosOffset.z);
            _spell.CastSpell(castPos, _spellLevel);
            return true;
        }

        return false;
    }

    /// <summary>
    /// We upgrade the Spell with the given id in the SpellContainer
    /// </summary>
    /// <param name="_containerID"></param>
    public void UpgradeSpell(int _containerID)
    {
        if (spellList[_containerID].learned)
            spellList[_containerID].level++;
        else
            spellList[_containerID].learned = true;
    }

    /// <summary>
    /// We pause this Code
    /// </summary>
    public void PauseCode()
    {
        isPaused = true;
    }

    /// <summary>
    /// We unpause this Code
    /// </summary>
    public void UnpauseCode()
    {
        isPaused = false;
    }
}
