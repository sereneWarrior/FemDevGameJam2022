using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject spawnPoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;

    void Start()
    {
        effectToSpawn = vfx[0];
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SpawnVFX();
        }
    }

    private void SpawnVFX()
    {
        GameObject vfx;

        vfx = Instantiate(effectToSpawn, spawnPoint.transform.position, Quaternion.identity);
    }
}
