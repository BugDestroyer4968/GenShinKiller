using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public PlayerInput playerInput { get; private set; }

    PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movementStateMachine = new PlayerMovementStateMachine(this);
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.idleState);
    }
    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();
    }
    private void FixedUpdate()
    {
        movementStateMachine.PhysicUpdate();
    }
}
