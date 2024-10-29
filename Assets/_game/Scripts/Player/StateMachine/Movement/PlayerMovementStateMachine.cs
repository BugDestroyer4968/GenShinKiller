using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player {get;}
    public PlayerStateReusebleData playerStateReusebleData {get;} 

    public PlayerIdleState idleState { get; }
    public PlayerWalkingState walkingState { get; }
    public PlayerRuningState runningState { get; }
    public PlayerSprintingState sprintingState { get; }

    public PlayerMovementStateMachine(Player player) 
    {
        this.player = player;
        playerStateReusebleData = new PlayerStateReusebleData();

        idleState = new PlayerIdleState( this);
        walkingState = new PlayerWalkingState( this);
        runningState = new PlayerRuningState( this);
        sprintingState = new PlayerSprintingState( this);
    }
}
