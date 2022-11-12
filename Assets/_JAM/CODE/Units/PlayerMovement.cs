using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;

    PlayerInput playerInput;
    Rigidbody ownRigidbody;

    Vector3 input;

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
        ownRigidbody.MovePosition(ownRigidbody.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        playerInput.onPlayerInput -= GetInput;
    }
}
