using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IDamagable
{
    [Header("DESIGN")]
    [Tooltip("Cur Health of this Unit")]
    public float curHealth;
    [Tooltip("The Max Health this unit can have")]
    public int maxHealth = 10;

    // Callbacks
    public delegate void UnitCallbacks();
    public UnitCallbacks onUnitDeath;
    public UnitCallbacks onUnitDisable;

    public delegate void UnitHealthCallbacks(float _curHealth);
    public UnitHealthCallbacks onUnitHealhChanged;

    public Collider myCollider;

    private void OnEnable()
    {
        SetupUnit();
    }

    private void OnDisable()
    {
        if (onUnitDisable != null)
            onUnitDisable();

        GameManger.RemoveUnit(this);
    }

    private void SetupUnit()
    {
        curHealth = maxHealth;
        if(myCollider)
            myCollider.enabled = true;
    }

    /// <summary>
    /// We get damage
    /// </summary>
    /// <param name="_damage"></param>
    public void GetDamage(float _damage)
    {
        Debug.Log("Getting Damage: " + _damage);

        curHealth -= _damage;

        if (onUnitHealhChanged != null)
            onUnitHealhChanged(curHealth);

        if (curHealth <= 0)
            Die();
    }

    /// <summary>
    /// This Entity Dies
    /// </summary>
    public void Die()
    {
        // We change the Health
        curHealth = 0;

        // We deactivate this Object
        if(myCollider)
            myCollider.enabled = false;

        // We send the Events
        if (onUnitDeath != null)
            onUnitDeath();
        GameManger.SendUnitDeathEvent(this);

        if(gameObject != null)
            gameObject.SetActive(false);
    }
}
