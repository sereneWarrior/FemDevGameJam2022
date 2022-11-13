using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualRandomizer : MonoBehaviour
{
    [Header("REFERENCES")]
    public List<GameObject> randomVisuals = new List<GameObject>();

    public void OnEnable()
    {
        EnableRandomVisual();
    }

    /// <summary>
    /// We disable all visuals first
    /// </summary>
    private void DisableAllVisuals()
    {
        foreach (GameObject visual in randomVisuals)
        {
            visual.SetActive(false);
        }
    }

    /// <summary>
    /// We Enable a random visual
    /// </summary>
    private void EnableRandomVisual()
    {
        DisableAllVisuals();
        randomVisuals[Random.Range(0, randomVisuals.Count)].SetActive(true);
    }
}
