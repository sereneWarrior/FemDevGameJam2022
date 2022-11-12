using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, IPausable
{
    [Header("DESIGN")]
    [Tooltip("Size of the Spawn Area")]
    public Rect spawnArea;
    [Tooltip("The Progress we make on the Spawncurve timelime each Frame")]
    public float spawnCurveSteps = 0.01f;
    [Tooltip("Curve that defines the Spawn cooldown")]
    public AnimationCurve spawnCdCurve;

    [Header("DATA")]
    public bool isPaused = false;
    private float spawnCurvePos;
    private float spawnTimer;
    public Queue<Unit> unitsPoolingQueue = new Queue<Unit>();

    [Header("REFERENCES")]
    public GameObject unitPrefab;

    public static EnemyHandler instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameManger.onPauseGame += PauseCode;
        GameManger.onUnpauseGame += UnpauseCode;
    }

    private void OnDisable()
    {
        GameManger.onPauseGame -= PauseCode;
        GameManger.onUnpauseGame -= UnpauseCode;
    }

    /// <summary>
    /// We spawn a unit on the given Position. Returns the Unit for future usage
    /// </summary>
    /// <param name="_spawnPos"></param>
    /// <returns></returns>
    public Unit SpawnUnit(Vector3 _spawnPos)
    {
        // We check if the pooling Queue is filled
        if (unitsPoolingQueue.Count <= 0)
            CreateNewUnit();

        // We spawn a new Unit
        Unit tempUnit = unitsPoolingQueue.Dequeue();
        tempUnit.transform.position = _spawnPos;
        tempUnit.gameObject.SetActive(true);

        // We return the Unit
        return tempUnit;
    }

    /// <summary>
    /// We create a new unit
    /// </summary>
    private void CreateNewUnit()
    {
        Unit tempUnit = Instantiate(unitPrefab, transform).GetComponent<Unit>();
        tempUnit.gameObject.SetActive(false);
        unitsPoolingQueue.Enqueue(tempUnit);
    }

    private void Update()
    {
        if (isPaused)
            return;

        CheckTimer();
        CheckSpawnCurveProgress();
    }

    /// <summary>
    /// We check the Spawn Curve Progress
    /// </summary>
    private void CheckSpawnCurveProgress()
    {
        // If we already reached the Max we return
        if(spawnCurvePos >= 1)
        {
            spawnCurvePos = 1;
            return;
        }

        // Else we up the Curve
        spawnCurvePos += spawnCurveSteps * Time.deltaTime;
    }

    /// <summary>
    /// We check the Timer
    /// </summary>
    private void CheckTimer()
    {
        if (spawnTimer <= 0)
        {
            SpawnUnit(new Vector3(Random.Range(spawnArea.x, spawnArea.width), 0, Random.Range(spawnArea.y, spawnArea.height)));
            spawnTimer = spawnCdCurve.Evaluate(spawnCurvePos);
        }
        else
            spawnTimer -= Time.deltaTime;
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
