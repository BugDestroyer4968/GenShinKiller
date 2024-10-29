using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovingState
{
    public PlayerWalkingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IStateMethods
    public override void Enter()
    {
        base.Enter();

        stateMachine.playerStateReusebleData.MovementSpeedMotifier = movementData.WalkData.speedMotifier;
    }
    #endregion
    
    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);

        stateMachine.ChangeState(stateMachine.runningState);
    }
    
    #endregion
}
