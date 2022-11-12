using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardUIHandler : MonoBehaviour
{
    [Header("DESIGN")]
    public float cardSlideDuration = 0.1f;
    public Ease moveEase;

    [Header("DATA")]
    private LevelUpCardUI[] spawnedCards = new LevelUpCardUI[3];

    [Header("REFERENCES")]
    public LevelUpCardUI levelUpCardPrefab;
    public RectTransform cardSpawnRect;
    public RectTransform[] cardTargetPos;

    public static CardUIHandler instance;

    public void Awake()
    {
        instance = this;
        InitiateCards();
    }

    /// <summary>
    /// We Initiate the Cards
    /// </summary>
    private void InitiateCards()
    {
        spawnedCards[0] = Instantiate(levelUpCardPrefab, cardSpawnRect.position, Quaternion.identity, transform);
        spawnedCards[1] = Instantiate(levelUpCardPrefab, cardSpawnRect.position, Quaternion.identity, transform);
        spawnedCards[2] = Instantiate(levelUpCardPrefab, cardSpawnRect.position, Quaternion.identity, transform);
    }

    /// <summary>
    /// We Setup and show the Cards
    /// </summary>
    /// <param name="_containerIDs"></param>
    /// <param name="_caster"></param>
    public void ShowCards(List<int> _containerIDs, SpellCaster _caster)
    {
        DOTween.Kill(gameObject);
        Sequence showSequence = DOTween.Sequence();
        GameManger.SendPauseGameEvent();

        for (int i = 0; i < spawnedCards.Length; i++)
        {
            // We position the card at the bottom of the screen
            spawnedCards[i].transform.position = cardSpawnRect.position;

            // We check if we should show this card
            if (i >= _containerIDs.Count)
                continue;

            // We show this card
            spawnedCards[i].SetupCard(_containerIDs[i], _caster);
            showSequence.Append(spawnedCards[i].transform.DOMove(cardTargetPos[i].position, cardSlideDuration).SetEase(moveEase));
        }

        showSequence.AppendCallback(ActivateAllCardCollider);
        showSequence.Play();
    }

    /// <summary>
    /// We Hide the Cards
    /// </summary>
    public void HideCards()
    {
        DOTween.Kill(gameObject);
        Sequence showSequence = DOTween.Sequence();

        for (int i = 0; i < spawnedCards.Length; i++)
        {
            showSequence.Append(spawnedCards[i].transform.DOMove(cardSpawnRect.position, cardSlideDuration));
            spawnedCards[i].ToggleCollider(false);
        }

        showSequence.Play();
    }

    /// <summary>
    /// We activate the collider of each Card
    /// </summary>
    private void ActivateAllCardCollider()
    {
        for (int i = 0; i < spawnedCards.Length; i++)
        {
            spawnedCards[i].ToggleCollider(true);
        }
    }
}
