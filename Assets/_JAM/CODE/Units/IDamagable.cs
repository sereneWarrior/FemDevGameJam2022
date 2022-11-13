using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>
    /// Entity gets Damage
    /// </summary>
    /// <param name="_damage"></param>
    public abstract void GetDamage(float _damage);

    /// <summary>
    /// This entity dies
    /// </summary>
    public abstract void Die();
}
