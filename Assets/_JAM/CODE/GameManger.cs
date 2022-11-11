using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManger
{
    [Header("REFERENCES")]
    public static Transform playerTrans;
    public static IDamagable playerDamagable;
    public static List<IDamagable> unitList = new List<IDamagable>();

    // Events
    public delegate void UnitEvents(Unit _unit);
    public static event UnitEvents onUnitSpawn;
    public static event UnitEvents onUnitDeath;

    /// <summary>
    /// We add a unit to the List
    /// </summary>
    /// <param name="_unit"></param>
    public static void AddUnit(Unit _unit)
    {
        unitList.Add(_unit);
    }

    /// <summary>
    /// We remove a Unit from the List
    /// </summary>
    /// <param name="_unit"></param>
    public static void RemoveUnit(Unit _unit)
    {
        unitList.Remove(_unit);
    }

}
