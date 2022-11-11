using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnitHealthbarUI : MonoBehaviour
{
    [Header("DESIGN")]
    [Tooltip("How much we add to the local rotation")]
    public Vector3 punchRotation;
    [Tooltip("Duration of the Punch feedback effect")]
    public float punchDuration = 1;
    [Tooltip("Can be used to offset the position of the Healthbar")]
    public Vector3 offset;

    [Header("REFERENCES")]
    [Header("The Main Health Slider")]
    public Slider mainSliderUI;
    private RectTransform mainSliderRect;
    [Header("The unit this Slider should represent")]
    private Unit unit;

    private void Awake()
    {
        mainSliderRect = mainSliderUI.GetComponent<RectTransform>();
    }

    /// <summary>
    /// We set the Slider values and subscribe to callbacks
    /// </summary>
    /// <param name="_unit"></param>
    public void SetupHealthBar(Unit _unit)
    {
        // We set the Slider Values
        mainSliderUI.maxValue = _unit.maxHealth;
        mainSliderUI.value = _unit.curHealth;

        // We save the Unit reference and subscribe to the Life change callback
        unit = _unit;
        unit.onUnitHealhChanged += UpdateSlider;
        unit.onUnitDisable += DisableBar;
    }

    /// <summary>
    /// We unsubscribe from a callback when we disable this Object
    /// </summary>
    public void OnDisable()
    {
        unit.onUnitHealhChanged -= UpdateSlider;
        unit.onUnitDisable -= DisableBar;
    }

    public void FixedUpdate()
    {
        UpdateHealthBarPos();
    }

    /// <summary>
    /// We update the Healthbar position on the Screen
    /// </summary>
    public void UpdateHealthBarPos()
    {
        // If we have no unit we return
        if (unit == null)
            return;

        // We convert the World to Screen Pos
        transform.position = Camera.main.WorldToScreenPoint(unit.transform.position + offset);
    }

    /// <summary>
    /// We visually update the Slider to represent the Health
    /// </summary>
    /// <param name="_curHealth"></param>
    public void UpdateSlider(float _curHealth)
    {
        mainSliderUI.value = _curHealth;
        mainSliderRect.DOPunchRotation(punchRotation, punchDuration);
    }

    /// <summary>
    /// Gets called when the Linked Unit gets disabled
    /// </summary>
    public void DisableBar()
    {
        UnitHealthBarHandler.instance.healthBarQueue.Enqueue(this);
        this.gameObject.SetActive(false);
    }
}