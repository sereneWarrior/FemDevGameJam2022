using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyHitDeformer : MonoBehaviour
{
    [Header("DESIGN")]
    public float strength = 1;
    public float duration = 0.1f;

    [Header("DATA")]
    private Vector3 baseSize;

    [Header("REFERENCES")]
    public Unit unit;

    private void OnEnable()
    {
        unit.onUnitHealhChanged += OnHealthChange;
    }

    private void OnDisable()
    {
        unit.onUnitHealhChanged -= OnHealthChange;
    }

    private void Awake()
    {
        baseSize = transform.localScale;
    }

    private void OnHealthChange(float _curHealth)
    {
        transform.DOShakeScale(duration, strength);
    }
}
