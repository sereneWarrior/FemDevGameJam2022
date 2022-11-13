using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Unit))]
public class EnemyAI : MonoBehaviour, IPausable
{
    [Header("DATA")]
    public bool isPaused = false;

    [Header("REFERENCES")]
    private Unit unit;
    private Unit playerUnit;
    private NavMeshAgent agent;

    [Header("ATTACK BEHAVIOUR")]
    [SerializeField] private float attackDistance = 1.25f;
    [SerializeField] private float damage = 1;
    [SerializeField] private float damageCooldown = 1;

    float currentCooldown = 0;
    private Transform ownTransform;
    private Transform playerTransform;

    public void Awake()
    {
        unit = this.GetComponent<Unit>();
        agent = this.GetComponent<NavMeshAgent>();

        ownTransform = transform;
    }
    private void OnEnable()
    {
        UnitHealthBarHandler.instance.RequestHealthBar(unit, false);
        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    private void OnDisable()
    {
        EnemyHandler.instance.unitsPoolingQueue.Enqueue(unit);
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    private void Start()
    {
        playerUnit = GameManger.playerUnit;
        playerTransform = playerUnit.transform;
    }

    private void Update()
    {
        if (isPaused)
            return;

        currentCooldown -= Time.deltaTime;
        if(playerTransform != null)
            if(currentCooldown <= 0 && (ownTransform.position - playerTransform.position).sqrMagnitude <= attackDistance * attackDistance)
            {
                currentCooldown = damageCooldown;
                playerUnit.GetDamage(damage);
            }
    }

    private void FixedUpdate()
    {
        if (isPaused)
            return;

        Move(playerUnit.transform.position);
    }

    /// <summary>
    /// We move towards a target
    /// </summary>
    /// <param name="_pos"></param>
    public void Move(Vector3 _pos)
    {
        agent.SetDestination(_pos);
    }

    /// <summary>
    /// We pause this Code
    /// </summary>
    public void PauseCode()
    {
        isPaused = true;
        if (agent.isActiveAndEnabled)
            agent.isStopped = true;
    }

    /// <summary>
    /// We unpause this Code
    /// </summary>
    public void UnpauseCode()
    {
        isPaused = false;
        if(agent.isActiveAndEnabled)
            agent.isStopped = false;
    }
}
