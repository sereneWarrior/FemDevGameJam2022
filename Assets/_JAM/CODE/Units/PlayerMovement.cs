using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPausable
{
    [SerializeField] private Transform visuals;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 250;

    PlayerInput playerInput;
    Rigidbody ownRigidbody;

    Vector3 input;
    Vector3 offset = new Vector3(90,0,0);

    [Header("DATA")]
    public bool isPaused = false;

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

    private void Awake()
    {
        ownRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onPlayerInput += GetInput;
    }

    void GetInput(Vector3 input)
    {
        this.input = input;
    }

    void FixedUpdate()
    {
        if (isPaused)
            return;

        ownRigidbody.MovePosition(ownRigidbody.position + input * moveSpeed * Time.fixedDeltaTime);
        if(visuals != null && input.sqrMagnitude > 0.1f)
            visuals.rotation = Quaternion.RotateTowards(visuals.rotation, Quaternion.LookRotation(input), turnSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        playerInput.onPlayerInput -= GetInput;
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
