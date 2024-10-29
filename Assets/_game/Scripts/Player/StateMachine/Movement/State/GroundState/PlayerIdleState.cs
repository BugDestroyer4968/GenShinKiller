using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region IStateMethods
    public override void Enter()
    {
        base.Enter();

        stateMachine.playerStateReusebleData.MovementSpeedMotifier = 0f;

        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();
        if(stateMachine.playerStateReusebleData.MovementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }
    #endregion
}
