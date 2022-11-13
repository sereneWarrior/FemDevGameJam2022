using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UITweenValue[] tweenOptions;

    private RectTransform ownRectTransform;

    private void Awake()
    {
        ownRectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ownRectTransform.rotation = Quaternion.identity;
        ownRectTransform.localScale = Vector3.one;
        ownRectTransform.DOKill();
        if(tweenOptions.Length < 1)
        {
            ownRectTransform.DOScale(ownRectTransform.localScale * 1.5f, 0.25f);
        }
        foreach(UITweenValue tweenOption in tweenOptions)
        {
            switch (tweenOption.tweenType)
            {
                case UITweenOptions.Shake:
                    ownRectTransform.DOShakeRotation(tweenOption.duration, tweenOption.strength, 10, 90, true, ShakeRandomnessMode.Harmonic);
                    break;
                case UITweenOptions.Scale:
                    ownRectTransform.DOScale(ownRectTransform.localScale * tweenOption.strength, tweenOption.duration);
                    break;
                /*case UITweenOptions.Bounce:
                    ownRectTransform.DOPunchScale(ownRectTransform.localScale * tweenOption.strength, tweenOption.duration);
                    break;*/
                default:
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ownRectTransform.rotation = Quaternion.identity;
        ownRectTransform.DOKill();
        if (tweenOptions.Length < 1)
        {
            ownRectTransform.DOScale(Vector3.one, 0.25f);
        }
        foreach (UITweenValue tweenOption in tweenOptions)
        {
            if (!tweenOption.onlyEnter)
            {
                switch (tweenOption.tweenType)
                {
                    case UITweenOptions.Shake:
                        ownRectTransform.DOShakeRotation(tweenOption.duration, tweenOption.strength, 10, 90, true, ShakeRandomnessMode.Harmonic);
                        break;
                    case UITweenOptions.Scale:
                        ownRectTransform.DOScale(Vector3.one, tweenOption.duration);
                        break;
                    /*case UITweenOptions.Bounce:
                        ownRectTransform.localScale = Vector3.one;
                        ownRectTransform.DOPunchScale(ownRectTransform.localScale * tweenOption.strength, tweenOption.duration);
                        break;*/
                    default:
                        break;
                }
            }
        }
    }
}

[System.Serializable]
public struct UITweenValue
{
    public UITweenOptions tweenType;
    public float duration;
    public float strength;
    public bool onlyEnter;
}

public enum UITweenOptions
{
    Shake,
    Scale,
    //Bounce,
}
