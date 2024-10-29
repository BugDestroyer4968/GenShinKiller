using System;
using UnityEngine;

[Serializable]
public class PlayerGroudedData 
{
    [field:SerializeField] [field:Range(0f,25f)]public float baseSpeed {get; private set;} =5f;
    [field:SerializeField] public PlayerRotationData BaseRotationData {get; private set;}
    [field:SerializeField] public PlayerWalkData WalkData {get; private set;}
    [field:SerializeField] public PlayerRunData RunData {get; private set;}
}
