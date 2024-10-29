using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
    {
    }
    #region Reusable Methods
    protected override void AddInoutActionsCallback()
    {
        base.AddInoutActionsCallback();

        stateMachine.player.playerInput.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionsCallback()
    {
        base.RemoveInputActionsCallback();
        stateMachine.player.playerInput.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }    

    protected virtual void OnMove()
    {
        if(stateMachine.playerStateReusebleData.shouldWalk)
        {
            stateMachine.ChangeState(stateMachine.walkingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion
    #region Input Methods

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.idleState);
    }
    #endregion
}
