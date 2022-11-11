using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;

    PlayerInput playerInput;
    Rigidbody ownRigidbody;

    private void Awake()
    {
        ownRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onPlayerInput += MovePlayer;
    }

    void MovePlayer(Vector3 input)
    {
        ownRigidbody.MovePosition(ownRigidbody.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        playerInput.onPlayerInput -= MovePlayer;
    }
}
