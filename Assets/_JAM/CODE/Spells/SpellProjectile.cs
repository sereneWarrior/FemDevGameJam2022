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
    [Tooltip("Speed of the Projectile")]
    private float speed;
    [Tooltip("SpellData of the Projectile Spell")]
    private SpellStats spellStats;
    private int projectileID;
    private bool madeDamage = false;

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
        SpellProjectileHandler.instance.spellProjectilePoolList[projectileID].poolingQueue.Enqueue(this);
    }

    /// <summary>
    /// We Setup the Projectile
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_speed"></param>
    /// <param name="_spellStats"></param>
    /// <param name="_id"></param>
    public void SetupProjectile(Unit _target, float _speed, SpellStats _spellStats, int _id)
    {
        targetUnit = _target;
        speed = _speed;
        spellStats = _spellStats;
        projectileID = _id;

        madeDamage = false;
        UnpauseCode();

        targetUnit.onUnitDisable += OnUnitDeath;
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
        }
    }

    /// <summary>
    /// We damage the Target
    /// </summary>
    public void MakeDamage()
    {
        if (madeDamage)
            return;

        if (spellStats.splashRadius == 0)
        {
            targetUnit.GetDamage(spellStats.damage);
            madeDamage = true;
        }
        
        DisableProjectile();
    }

    /// <summary>
    /// Gets called when we make Splash Damage
    /// </summary>
    private void MakeSplashDamage()
    {
        if (madeDamage)
            return;

        if (spellStats.splashRadius > 0)
            Utilities.DamageAllInRange(targetUnit.transform.position, spellStats.splashRadius, spellStats.damage, GameManger.enemyLayer);

        if (splashVFX == null || targetUnit == null)
            return;

        Vector3 splashPos = new Vector3(targetUnit.transform.position.x + spawnOffset.x, targetUnit.transform.position.y + spawnOffset.y, targetUnit.transform.position.z + spawnOffset.z);
        Instantiate(splashVFX, splashPos, Quaternion.identity);

        madeDamage = true;
    }

    /// <summary>
    /// Gets called when the Unit Dies
    /// </summary>
    private void OnUnitDeath()
    {
        DisableProjectile();
    }

    /// <summary>
    /// We disable this Projectile
    /// </summary>
    private void DisableProjectile()
    {
        MakeSplashDamage();
        LooseTarget();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// We loose our Target
    /// </summary>
    private void LooseTarget()
    {
        if (targetUnit)
        {
            targetUnit.onUnitDisable -= DisableProjectile;
            targetUnit = null;
        }
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
