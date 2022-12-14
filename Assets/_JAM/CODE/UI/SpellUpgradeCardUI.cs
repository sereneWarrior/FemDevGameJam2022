using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SpellUpgradeCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("DESIGN")]
    public Color baseColor;
    public Color highlightColor;
    public float shakeDuration = 0.2f;
    public float shakeStrength = 0.1f;

    [Header("DATA")]
    public bool isInteractable = false;
    private Tween shakeTween;

    [Header("UI REFERENCES")]
    public TMP_Text nameDisplay;
    public TMP_Text levelDisplay;
    public TMP_Text damageDisplay;
    public TMP_Text cooldownDisplay;
    public TMP_Text rangeDisplay;
    public TMP_Text splashDisplay;
    private Vector3 preshakePos;

    [Header("REFERENCES")]
    public Image imageRenderer;
    private SpellCaster caster;
    private int containerID;

    /// <summary>
    /// We Setup this Card
    /// </summary>
    /// <param name="_containerID"></param>
    /// <param name="_caster"></param>
    public void SetupCard(int _containerID, SpellCaster _caster)
    {
        // We get the Data we need
        int spellLevel = _caster.spellList[_containerID].level;
        BaseSpell spellReference = _caster.spellList[_containerID].spellReference;
        caster = _caster;
        containerID = _containerID;

        // We reset some Stuff
        ToggleHighlight(false);

        // We set the base display Data
        nameDisplay.text = "<color=#1C8E40>" + spellReference.spellName;
        levelDisplay.text = spellLevel.ToString();
        damageDisplay.text = spellReference.leveledSpellStats[spellLevel].damage.ToString();
        cooldownDisplay.text = spellReference.leveledSpellStats[spellLevel].cooldown.ToString();
        rangeDisplay.text = spellReference.leveledSpellStats[spellLevel].range.ToString();
        splashDisplay.text = spellReference.leveledSpellStats[spellLevel].splashRadius.ToString();

        // If we have not learned this skill yet we return
        if (!_caster.spellList[_containerID].learned)
            return;

        Debug.Log(spellLevel);

        // We display the Upgrade Stats
        nameDisplay.text = spellReference.spellName;
        levelDisplay.text = spellLevel + " => <color=#1C8E40>" + (spellLevel + 1);
        damageDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].damage;
        cooldownDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].cooldown;
        rangeDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].range;
        splashDisplay.text += " => <color=#1C8E40>" + spellReference.leveledSpellStats[spellLevel + 1].splashRadius;
    }

    /// <summary>
    /// We toggle the interactivity of this Card
    /// </summary>
    /// <param name="_isActive"></param>
    public void ToggleInteractivity(bool _isActive)
    {
        isInteractable = _isActive;
    }

    /// <summary>
    /// We toggle the Highlight of this Card
    /// </summary>
    /// <param name="_isActive"></param>
    public void ToggleHighlight(bool _isActive)
    {
        if(_isActive)
        {
            ShakeCard();
            imageRenderer.color = highlightColor;
        }
        else
            imageRenderer.color = baseColor;
    }

    /// <summary>
    /// We shake the Card
    /// </summary>
    private void ShakeCard()
    {
        // We first stop the Shake
        StopShake();

        // We then start the Shake again
        preshakePos = transform.position;
        shakeTween = transform.DOShakePosition(shakeDuration, shakeStrength);
    }

    /// <summary>
    /// We stop the Shake
    /// </summary>
    private void StopShake()
    {
        if (shakeTween != null)
        {
            shakeTween.Kill();
            transform.position = preshakePos;
        }
    }

    /// <summary>
    /// We execute this Upgrade
    /// </summary>
    private void ExecuteUpgrade()
    {
        caster.UpgradeSpell(containerID);
        CardUIHandler.instance.HideCards();
    }

    /// <summary>
    /// Implementation of Unitys OnPointerEnter
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable)
            return;

        ToggleHighlight(true);
    }

    /// <summary>
    /// Implementation of Unitys OnPointerExit
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable)
            return;

        ToggleHighlight(false);
    }

    /// <summary>
    /// Implementation of Unitys OnPointerClick
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable)
            return;

        ExecuteUpgrade();
    }
}
