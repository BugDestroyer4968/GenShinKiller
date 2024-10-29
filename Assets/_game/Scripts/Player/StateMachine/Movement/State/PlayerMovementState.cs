using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected PlayerGroudedData movementData;

    public PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        movementData = stateMachine.player.Data.groundedData;

        InitializeData();
    }

    private void InitializeData()
    {
        stateMachine.playerStateReusebleData.TimeToReachRotation = movementData.BaseRotationData.TargetRotationReachTime;
    }
    #region IState
    public virtual void Enter()
    {
        AddInoutActionsCallback();
    }



    public virtual void Exit()
    {
        RemoveInputActionsCallback();
    }



    public virtual void HandleInput()
    {
        readMovementInput();
    }


    public virtual void PhysicUpdate()
    {
        Move();
    }



    public virtual void Update()
    {

    }
    #endregion
    #region MainMethods
    private void readMovementInput()
    {
        stateMachine.playerStateReusebleData.MovementInput = stateMachine.player.playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }
    private void Move()
    {
        if (stateMachine.playerStateReusebleData.MovementInput == Vector2.zero || stateMachine.playerStateReusebleData.MovementSpeedMotifier == 0f)
        {
            return;
        }
        Vector3 movementDirections = GetMovementInputDirection();

        float targetRatationYAngle = Rotate(movementDirections);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRatationYAngle);

        float momentSpeed = GetMovementSpeed();

        Vector3 currentVelocity = GetPlayerVelocity();

        stateMachine.player.rb.AddForce(momentSpeed * targetRotationDirection - currentVelocity, ForceMode.VelocityChange);
    }



    private float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRatation(direction);

        RotateTowardTargetRotation();

        return directionAngle;
    }



    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.playerStateReusebleData.CurrentTargetRotation.y = targetAngle;

        stateMachine.playerStateReusebleData.DampedTargetRotationPassedTime.y = 0f;
    }

    private float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.player.MainCameraTransform.eulerAngles.y;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }




    #endregion
    #region ReusableMethods
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.playerStateReusebleData.MovementInput.x, 0f, stateMachine.playerStateReusebleData.MovementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return movementData.baseSpeed * stateMachine.playerStateReusebleData.MovementSpeedMotifier;
    }
    protected Vector3 GetPlayerVelocity()
    {
        Vector3 playerVelocity = stateMachine.player.rb.velocity;
        playerVelocity.y = 0f;
        return playerVelocity;
    }
    protected void RotateTowardTargetRotation()
    {
        float currentYAngle = stateMachine.player.transform.eulerAngles.y;

        if (currentYAngle == stateMachine.playerStateReusebleData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.playerStateReusebleData.CurrentTargetRotation.y, ref stateMachine.playerStateReusebleData.DampedTargetRotationVelocity.y,
             stateMachine.playerStateReusebleData.TimeToReachRotation.y - stateMachine.playerStateReusebleData.DampedTargetRotationPassedTime.y);

        stateMachine.playerStateReusebleData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion TargetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

        stateMachine.player.rb.MoveRotation(TargetRotation);
    }
    protected float UpdateTargetRatation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != stateMachine.playerStateReusebleData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }
    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    protected void ResetVelocity()
    {
        stateMachine.player.rb.velocity = Vector3.zero;
    }
    protected virtual void AddInoutActionsCallback()
    {
        stateMachine.player.playerInput.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
    }



    protected virtual void RemoveInputActionsCallback()
    {
        stateMachine.player.playerInput.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
    }
    #endregion
    #region InputMethods
    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        stateMachine.playerStateReusebleData.shouldWalk = !stateMachine.playerStateReusebleData.shouldWalk;
    }
    #endregion
}
