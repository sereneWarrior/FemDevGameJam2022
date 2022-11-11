using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBarHandler : MonoBehaviour
{
    [Header("DATA")]
    public Queue<UnitHealthbarUI> healthBarQueue = new Queue<UnitHealthbarUI>();

    [Header("REFERENCES")]
    public GameObject healthBarPrefab;

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
    public void RequestHealthBar(Unit _unit)
    {
        if (healthBarQueue.Count <= 0)
            CreateNewHealthbar();

        // We get the Healthbar and Set everything up
        UnitHealthbarUI tempBar = healthBarQueue.Dequeue();
        tempBar.SetupHealthBar(_unit);
        tempBar.gameObject.SetActive(true);
    }

    /// <summary>
    /// We create and enqueue a New Health Bar
    /// </summary>
    public void CreateNewHealthbar()
    {
        healthBarQueue.Enqueue(Instantiate(healthBarPrefab, this.transform).GetComponent<UnitHealthbarUI>());
    }
}

