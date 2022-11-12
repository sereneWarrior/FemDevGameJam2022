using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardHandlerUI : MonoBehaviour
{
    [Header("DESIGN")]
    public float cardSlideDuration = 0.1f;

    [Header("DATA")]
    private LevelUpCardUI[] spawnedCards = new LevelUpCardUI[3];

    [Header("REFERENCES")]
    public LevelUpCardUI levelUpCardPrefab;
    public RectTransform cardSpawnRect;
    public RectTransform[] cardTargetPos;

    public void Awake()
    {
        InitiateCards();
    }

    public void InitiateCards()
    {
        spawnedCards[0] = Instantiate(levelUpCardPrefab, transform);
        spawnedCards[1] = Instantiate(levelUpCardPrefab, transform);
        spawnedCards[2] = Instantiate(levelUpCardPrefab, transform);
    }

    /// <summary>
    /// We show the Cards
    /// </summary>
    public void ShowCards()
    {
        DOTween.Kill(gameObject);

        for (int i = 0; i < spawnedCards.Length; i++)
        {
            spawnedCards[i].transform.position = cardSpawnRect.position;
            spawnedCards[i].gameObject.SetActive(true);
            spawnedCards[i].transform.DOMove(cardTargetPos[i].position, cardSlideDuration);
        }
    }

    /// <summary>
    /// We Hide the Cards
    /// </summary>
    public void HideCards()
    {
        DOTween.Kill(gameObject);

        for (int i = 0; i < spawnedCards.Length; i++)
        {
            spawnedCards[i].transform.DOMove(cardSpawnRect.position, cardSlideDuration);       
        }
    }

    public void Update()
    {
        // Just for Debugging
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ShowCards();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            HideCards();
    }
}
