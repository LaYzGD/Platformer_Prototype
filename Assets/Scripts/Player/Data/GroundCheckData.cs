using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/CheckData/GroundCheck", fileName = "GroundCheckData")]
public class GroundCheckData : ScriptableObject
{
    [field: SerializeField] public float VerticalVelocityTreshold { get; private set; }
    [field: SerializeField] public float CheckAngle { get; private set; }
    [field: SerializeField] public float CheckDistance { get; private set; }
    [field: SerializeField] public Vector2 CheckDirection { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
}