using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpellProjectile : MonoBehaviour, IPausable
{
    [Header("DESIGN")]
    [Tooltip("From what distance to the target to disable this")]
    public float targetReachedTreshold = 0.2f;

    [Header("DATA")]
    public bool isPaused = false;
    [Tooltip("How much damage this Projectile makes")]
    private float damage;
    [Tooltip("Speed of the Projectile")]
    private float speed;
    [Tooltip("The Splash radius of this Projectile")]
    private float splashRadius;

    [Header("SPLASH VFX")]
    [Tooltip("Offset spawn position in relation to our Target")]
    public Vector3 spawnOffset = Vector3.zero;
    [Tooltip("VFX we spawn when we do splash damage")]
    public GameObject splashVFX;

    [Header("REFERENCES")]
    private Unit targetUnit;

    public void OnEnable()
    {
        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    public void OnDisable()
    {
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame -= UnpauseCode;
    }

    /// <summary>
    /// We Setup the Projectile
    /// </summary>
    /// <param name="_unit"></param>
    /// <param name="_damage"></param>
    /// <param name="_speed"></param>
    /// <param name="_splashRadius"></param>
    public void SetupProjectile(Unit _unit, float _damage, float _speed, float _splashRadius = 0)
    {
        targetUnit = _unit;
        damage = _damage;
        speed = _speed;
        splashRadius = _splashRadius;
        targetUnit.onUnitDisable += DisableProjectile;
    }

    private void Update()
    {
        if (isPaused)
            return;

        MoveProjectile();
        CheckDistance();
    }

    /// <summary>
    /// We move the Projectile
    /// </summary>
    private void MoveProjectile()
    {
        transform.LookAt(targetUnit.transform);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    /// <summary>
    /// We check the Distance between us and the Target
    /// </summary>
    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, targetUnit.transform.position) <= targetReachedTreshold)
        {
            MakeDamage();
            DisableProjectile();
        }
    }

    /// <summary>
    /// We damage the Target
    /// </summary>
    public void MakeDamage()
    {
        if (splashRadius == 0)
            targetUnit.GetDamage(damage);
        else
        {
            Utilities.DamageAllInRange(targetUnit.transform.position, splashRadius, damage, GameManger.enemyLayer);
            OnSplashDamage();
        }

    }

    /// <summary>
    /// Gets called when we make Splash Damage
    /// </summary>
    private void OnSplashDamage()
    {
        if (splashVFX == null)
            return;

        Vector3 splashPos = new Vector3(targetUnit.transform.position.x + spawnOffset.x, targetUnit.transform.position.y + spawnOffset.y, targetUnit.transform.position.z + spawnOffset.z);
        Instantiate(splashVFX, splashPos, Quaternion.identity);
    }

    /// <summary>
    /// We disable this Projectile
    /// </summary>
    private void DisableProjectile()
    {
        targetUnit.onUnitDisable -= DisableProjectile;
        Destroy(this.gameObject); // Later we need to add pooling
    }

    /// <summary>
    /// We pause this Code
    /// </summary>
    public void PauseCode()
    {
        isPaused = true;
    }

    /// <summary>
    /// We unpause this Code
    /// </summary>
    public void UnpauseCode()
    {
        isPaused = false;
    }
}
