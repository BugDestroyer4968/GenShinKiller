using UnityEngine;

[CreateAssetMenu(fileName = "Player",menuName ="Custom/Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField]public PlayerGroudedData groundedData {get; private set;}
}
