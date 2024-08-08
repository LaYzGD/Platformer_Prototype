using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/StateData/AirState", fileName = "AirStateData")]
public class AirStateData : ScriptableObject
{
    [field: SerializeField] public float MaxFallVelocity { get; private set; }
    [field: SerializeField] public float MaxHorizontalVelocity { get; private set; }
    [field: SerializeField] public float FallVelocity { get; private set; }
    [field: SerializeField] public float JumpDownwardVelocity { get; private set; }
    [field: SerializeField] public float JumpHorizontalSpeed { get; private set; }
    [field: SerializeField] public float CoyoteeTime { get; private set; }
    [field: SerializeField] public float JumpMultiplier { get; private set; }
}
