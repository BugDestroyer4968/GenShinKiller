using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field:Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }
    public Rigidbody rb { get; private set; }
    public PlayerInput playerInput { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movementStateMachine = new PlayerMovementStateMachine(this);
        playerInput = GetComponent<PlayerInput>();

        MainCameraTransform = Camera.main.transform;
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
