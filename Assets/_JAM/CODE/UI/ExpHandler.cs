using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Later add exp curve
/// </summary>
public class ExpHandler : MonoBehaviour
{
    [Header("DESIGN")]
    [Tooltip("Max level we can reach")]
    public int maxLevel = 100;
    [Tooltip("Curve we use for the max exp each level")]
    public AnimationCurve maxExpCurve;

    [Header("DATA")]
    [Tooltip("Cur Level the Player currently has")]
    public int curLevel;
    [Tooltip("If we currently can gain exp")]
    private bool canGetExp = true;
    [Tooltip("The current exp the Player holds")]
    private float curExp = 0;
    [Tooltip("The max exp the player can has on this level")]
    private float maxExp;

    [Header("REFERENCES")]
    public Slider expSliderUI;
    public static ExpHandler instance;

    public delegate void ExpEvents();
    public static event ExpEvents onLevelUp;

    private void OnEnable()
    {
        GameManger.onUnitDeath += OnUnitDeath;
    }

    private void OnDisable()
    {
        GameManger.onUnitDeath -= OnUnitDeath;
    }

    private void Awake()
    {
        instance = this;
        SetupScript();
    }

    /// <summary>
    /// We setup this Script
    /// </summary>
    private void SetupScript()
    {
        curExp = 0;
        maxExp = maxExpCurve.Evaluate(0);
        UpdateSliderValues();
    }

    /// <summary>
    /// Gets called when a Unit dies
    /// </summary>
    /// <param name="_unit"></param>
    private void OnUnitDeath(Unit _unit)
    {
        GetExp(1);
    }

    /// <summary>
    /// Player gets exp
    /// </summary>
    /// <param name="_exp"></param>
    private void GetExp(float _exp)
    {
        if (!canGetExp)
            return;

        // We get exp and update the Slider Values
        curExp += _exp;
        UpdateSliderValues();

        if(curExp >= maxExp)
            OnLevelUp();
    }

    /// <summary>
    /// We Level up this Unit
    /// </summary>
    private void OnLevelUp()
    {
        // We update the Level
        curLevel++;

        // We send the Level up Event
        if (onLevelUp != null)
            onLevelUp();

        // We check if the Player reached max Level
        if(curLevel >= maxLevel)
        {
            OnMaxLevelReached();
            return;
        }

        // We update the Cur Exp
        float overflow = curExp - maxExp;
        curExp = overflow;
        maxExp = maxExpCurve.Evaluate(curLevel / maxLevel);

        // We update the Slider Values
        UpdateSliderValues();
    }

    /// <summary>
    /// Gets called when the Player reached max Level
    /// </summary>
    private void OnMaxLevelReached()
    {
        curExp = maxExp;
        canGetExp = false;
        UpdateSliderValues();
    }

    /// <summary>
    /// We update the Slider
    /// </summary>
    private void UpdateSliderValues()
    {
        if (expSliderUI == null)
            return;

        expSliderUI.maxValue = maxExp;
        expSliderUI.value = curExp;
    }
}
