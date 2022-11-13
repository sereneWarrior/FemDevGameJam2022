using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxLevelSetter : MonoBehaviour
{
    [Header("REFERENCES")]
    public SpellCaster spellCaster;

    private void Start()
    {
        CalculateMaxLevel();
    }

    /// <summary>
    /// We calculate the Max Level of the Player
    /// </summary>
    public void CalculateMaxLevel()
    {
        // We create the container
        int maxLevel = 0;

        // We calculate the Max Level
        foreach (SpellContainer spellContainer in spellCaster.spellList)
        {
            if (!spellContainer.learned)
                maxLevel++;

            maxLevel += (spellContainer.spellReference.leveledSpellStats.Count - 1) - spellContainer.level;
        }

        // Safety Check
        if (maxLevel == 0)
        {
            Debug.LogError("MaxLevelSetter: Max Level Calculation returned 0, this should not happen!");
            return;
        }

        // We set the max level
        ExpHandler.instance.maxLevel = maxLevel;
    }
}
