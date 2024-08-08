using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/CheckData/GroundCheck", fileName = "GroundCheckData")]
public class GroundCheckData : ScriptableObject
{
    [field: SerializeField] public float VerticalVelocityTreshold { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
}