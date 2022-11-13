using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SpellProjectileHandler : MonoBehaviour
{
    [Header("REFERENCES")]
    public List<SpellProjectilePool> spellProjectilePoolList = new List<SpellProjectilePool>();

    public static SpellProjectileHandler instance;

    private void OnEnable()
    {
        for (int i = 0; i < spellProjectilePoolList.Count; i++)
        {
            spellProjectilePoolList[i].id = i;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// We request a new Spell Projectile
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_spawnPos"></param>
    /// <returns></returns>
    public SpellProjectile RequestSpellProjectile(int _id, Vector3 _spawnPos, Unit _target, float _speed, SpellStats _spellStats)
    {
        if (spellProjectilePoolList[_id].poolingQueue.Count <= 0)
            CreateNewProjectile(_id);

        SpellProjectile spellProjectile = spellProjectilePoolList[_id].poolingQueue.Dequeue();
        spellProjectile.transform.position = _spawnPos;
        spellProjectile.SetupProjectile(_target, _speed, _spellStats, _id);
        spellProjectile.gameObject.SetActive(true);
        return spellProjectile;
    }

    /// <summary>
    /// We create a new Projectile
    /// </summary>
    /// <param name="_id"></param>
    private void CreateNewProjectile(int _id)
    {
        spellProjectilePoolList[_id].poolingQueue.Enqueue(Instantiate(spellProjectilePoolList[_id].projectilePrefab, transform));
    }
}

[System.Serializable]
public class SpellProjectilePool
{
    [HideInInspector]
    public int id;
    public SpellProjectile projectilePrefab;
    public Queue<SpellProjectile> poolingQueue = new Queue<SpellProjectile>();
}
