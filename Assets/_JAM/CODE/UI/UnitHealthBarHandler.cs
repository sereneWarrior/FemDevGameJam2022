using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBarHandler : MonoBehaviour
{
    [Header("DATA")]
    public Queue<UnitHealthbarUI> healthBarQueue = new Queue<UnitHealthbarUI>();

    [Header("REFERENCES")]
    public UnitHealthbarUI healthBarPrefab;

    [Header("ENEMY SLIDER COLORS")]
    [SerializeField] Color enemyMainSliderColor = Color.red;
    [SerializeField] Color enemyDelaySliderColor = new Color(0.5f, 0, 0, 1);
    [SerializeField] Color enemyBackgroundSliderColor = Color.grey;

    [Header("Player SLIDER COLORS")]
    [SerializeField] Color playerMainSliderColor = Color.green;
    [SerializeField] Color playerDelaySliderColor = new Color(0, 0.5f, 0, 1);
    [SerializeField] Color playerBackgroundSliderColor = Color.grey;

    // Static Reference to the Handler
    public static UnitHealthBarHandler instance;

    public void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// We request a Healthbar for our Unit
    /// </summary>
    /// <param name="_unit"></param>
    public void RequestHealthBar(Unit _unit, bool isPlayer)
    {
        if (healthBarQueue.Count <= 0)
            CreateNewHealthbar();

        // We get the Healthbar and Set everything up
        UnitHealthbarUI tempBar = healthBarQueue.Dequeue();
        if(isPlayer)
            tempBar.SetupHealthBar(_unit, enemyMainSliderColor, enemyDelaySliderColor, enemyBackgroundSliderColor);
        else
            tempBar.SetupHealthBar(_unit, playerMainSliderColor, playerDelaySliderColor, playerBackgroundSliderColor);
        tempBar.gameObject.SetActive(true);
    }

    /// <summary>
    /// We create and enqueue a New Health Bar
    /// </summary>
    public void CreateNewHealthbar()
    {
        healthBarQueue.Enqueue(Instantiate(healthBarPrefab, this.transform));
    }
}

