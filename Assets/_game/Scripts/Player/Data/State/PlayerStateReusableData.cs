using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusebleData
{
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedMotifier { get; set; } = 1f;

    public bool shouldWalk { get; set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachRotation;
    private Vector3 dampedTargetRotationVelocity;
    private Vector3 dampedTargetRotationPassedTime;

    public ref Vector3 CurrentTargetRotation => ref currentTargetRotation;
    public ref Vector3 TimeToReachRotation => ref timeToReachRotation;
    public ref Vector3 DampedTargetRotationVelocity => ref dampedTargetRotationVelocity;
    public ref Vector3 DampedTargetRotationPassedTime => ref dampedTargetRotationPassedTime;
}
