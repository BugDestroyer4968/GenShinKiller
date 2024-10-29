using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRuningState : PlayerMovingState
{
    public PlayerRuningState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IStateMethods
    public override void Enter()
    {
        base.Enter();

        stateMachine.playerStateReusebleData.MovementSpeedMotifier = movementData.RunData.speedMotifier;
    }
    #endregion
    
    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);

        stateMachine.ChangeState(stateMachine.walkingState);
    }
    
    #endregion
}
