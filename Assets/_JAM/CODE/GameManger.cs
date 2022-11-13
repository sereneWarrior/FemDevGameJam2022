using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManger
{
    [Header("DATA")]
    public static bool stopTime = false;
    public static int enemyLayer = 1 << 6;
    public static Unit playerUnit;
    public static List<IDamagable> unitList = new List<IDamagable>();

    // Unit Events
    public delegate void UnitEvents(Unit _unit);
    public static event UnitEvents onUnitSpawn;
    public static event UnitEvents onUnitDeath;

    public delegate void GameEvents();
    public static event GameEvents onPauseGame;
    public static event GameEvents onUnpauseGame;

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

    /// <summary>
    /// We send the Pause Game Event
    /// </summary>
    public static void SendPauseGameEvent()
    {
        if (onPauseGame != null)
            onPauseGame();
    }

    /// <summary>
    /// We send the Unpause Game Event
    /// </summary>
    public static void SendUnpauseGameEvent()
    {
        if (onUnpauseGame != null)
            onUnpauseGame();
    }

    /// <summary>
    /// We send the Unit Death Event
    /// </summary>
    /// <param name="_unit"></param>
    public static void SendUnitDeathEvent(Unit _unit)
    {
        if (onUnitDeath != null)
            onUnitDeath(_unit);
    }
}
