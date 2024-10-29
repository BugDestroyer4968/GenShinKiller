using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected Vector2 movementInput;
    protected float baseSpeed =5f;
    protected float speedMotifier = 1f;
    public PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    #region IState
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
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
       movementInput = stateMachine.player.playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }
    private void Move()
    {
        if(movementInput == Vector2.zero || speedMotifier ==0f)
        {
            return;
        }
        Vector3 movementDirections = GetMovementInputDirection();
        float momentSpeed = GetMovementSpeed();

        Vector3 currentVelocity = GetPlayerVelocity();

        stateMachine.player.rb.AddForce( momentSpeed  * movementDirections - currentVelocity, ForceMode.VelocityChange);
    }

    




    #endregion
    #region ReusableMethods
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return baseSpeed * speedMotifier;
    }
    protected Vector3 GetPlayerVelocity()
    {
        Vector3 playerVelocity = stateMachine.player.rb.velocity;
        playerVelocity.y = 0f;
        return playerVelocity;
    }
    #endregion
}
