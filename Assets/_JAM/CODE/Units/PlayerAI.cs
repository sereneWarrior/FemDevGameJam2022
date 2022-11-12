using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellCaster))]
[RequireComponent(typeof(Unit))]
public class PlayerAI : MonoBehaviour
{
    [Header("REFERENCES")]
    private Unit unit;
    private SpellCaster spellCaster;

    public void Awake()
    {
        unit = this.GetComponent<Unit>();
        spellCaster = this.GetComponent<SpellCaster>();
        GameManger.playerUnit = unit;
    }

    public void OnEnable()
    {
        UnitHealthBarHandler.instance.RequestHealthBar(unit, true);
        ExpHandler.onLevelUp += OnLevelUp;
    }

    public void OnDisable()
    {
        ExpHandler.onLevelUp -= OnLevelUp;
    }

    /// <summary>
    /// Gets called when the Player Levels up
    /// </summary>
    private void OnLevelUp()
    {
        Debug.Log("GOT LEVEL UP");

        // We get the possible Spell upgrades in a List
        List<int> possibleUpgrades = new List<int>();
        List<int> chosenUpgrades = new List<int>();

        // We get all spells that are currently upgradable
        for (int x = 0; x < spellCaster.spellList.Count; x++)
        {
            if (spellCaster.spellList[x].level < spellCaster.spellList[x].spellReference.leveledSpellStats.Count)
                possibleUpgrades.Add(x);
        }

        // We now get three random spells to Upgrade
        for (int i = 0; i < 3; i++)
        {
            // We check if the List is empty
            if (possibleUpgrades.Count == 0)
                break;

            // We get the Random value
            int randomIndex = Random.Range(0, possibleUpgrades.Count);
            chosenUpgrades.Add(possibleUpgrades[randomIndex]);
            possibleUpgrades.RemoveAt(randomIndex);
        }

        CardUIHandler.instance.ShowCards(chosenUpgrades, spellCaster);
    }
}
