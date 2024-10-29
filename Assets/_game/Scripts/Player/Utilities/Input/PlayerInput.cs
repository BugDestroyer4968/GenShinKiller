using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public PlayerMovement InputActions { get; private set; }

    public PlayerMovement.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerMovement();
        PlayerActions = InputActions.Player;
    }
    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }
}
