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
        UnitHealthBarHandler.instance.RequestHealthBar(unit);
    }
}
