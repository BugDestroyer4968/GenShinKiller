using System;
using UnityEngine;

[Serializable]
public class PlayerRunData 
{
    [field:SerializeField] [field:Range(1f,2f)] public float speedMotifier {get; private set;} = 1f;
}
