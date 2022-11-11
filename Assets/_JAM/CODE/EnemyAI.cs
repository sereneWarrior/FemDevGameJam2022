using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Unit))]
public class EnemyAI : MonoBehaviour
{
    [Header("REFERENCES")]
    private Unit unit;
    private Transform playerTrans;
    private IDamagable playerDamagable;
    private NavMeshAgent agent;

    public void Awake()
    {
        unit = this.GetComponent<Unit>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        playerTrans = GameManger.playerTrans;
        playerDamagable = GameManger.playerDamagable;
        UnitHealthBarHandler.instance.RequestHealthBar(unit);
        Move(playerTrans.position);
    }

    /// <summary>
    /// We move towards a target
    /// </summary>
    /// <param name="_pos"></param>
    public void Move(Vector3 _pos)
    {
        agent.SetDestination(_pos);
    }
}
