using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/StateData/MoveState", fileName = "MoveStateData")]
public class MoveStateData : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
}
