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
    [Tooltip("Time that has to pass in order to activate the Tween of the slider values")]
    public float sliderTweenWaitTime = 2f;
    [Tooltip("Time that the tweening of the values takes")]
    public float sliderTweenValueTime = 1f;

    [Header("REFERENCES")]
    [Header("Health Sliders")]
    public Image mainSliderUI;
    public Image delayedSliderUI;
    public Image backgroundSliderUI;
    [Header("The unit this Slider should represent")]
    private Unit unit;

    private RectTransform ownRectTransform;
    private Camera camMain;
    private float lastHitTime;
    private bool hasTweenedSlider = true;

    private void Awake()
    {
        ownRectTransform = gameObject.GetComponent<RectTransform>();
        camMain = Camera.main;
    }

    /// <summary>
    /// We set the Slider values and subscribe to callbacks
    /// </summary>
    /// <param name="_unit"></param>
    public void SetupHealthBar(Unit _unit, Color mainSliderColor, Color delaySliderColor, Color sliderBackgroundColor)
    {
        //Setup sliders
        mainSliderUI.color = mainSliderColor;
        delayedSliderUI.color = delaySliderColor;
        backgroundSliderUI.color = sliderBackgroundColor;
        mainSliderUI.fillAmount = 1;
        delayedSliderUI.fillAmount = 1;

        // We save the Unit reference and subscribe to the Life change callback
        unit = _unit;
        unit.onUnitHealhChanged += UpdateSlider;
        unit.onUnitDisable += DisableBar;

        // We update the Healthbar Pos
        UpdateHealthBarPos();
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
        CheckForSliderTween();
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
        transform.position = camMain.WorldToScreenPoint(unit.transform.position + offset);
    }

    void CheckForSliderTween()
    {
        if(!hasTweenedSlider && Time.time - lastHitTime > sliderTweenWaitTime)
        {
            hasTweenedSlider = true;
            DOTween.Kill(ownRectTransform);
            ownRectTransform.rotation = Quaternion.identity;
            ownRectTransform.localScale = Vector3.one;
            ownRectTransform.DOShakeScale(0.5f, 0.5f);
            delayedSliderUI.DOFillAmount(mainSliderUI.fillAmount, sliderTweenValueTime);
        }
    }

    /// <summary>
    /// We visually update the Slider to represent the Health
    /// </summary>
    /// <param name="_curHealth"></param>
    public void UpdateSlider(float _curHealth)
    {
        mainSliderUI.fillAmount = Utils.ConvertRange(0, unit.maxHealth, 0, 1, _curHealth);
        DOTween.Kill(ownRectTransform);
        ownRectTransform.rotation = Quaternion.identity;
        ownRectTransform.DOPunchRotation(punchRotation, punchDuration);
        lastHitTime = Time.time;
        hasTweenedSlider = false;
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