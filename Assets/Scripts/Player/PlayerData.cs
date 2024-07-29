using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/MainData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public float ImunityFramesTime { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float ThrowForce { get; private set; }
    [field: SerializeField] public GroundCheckData GroundCheckData { get; private set; }
    [field: SerializeField] public AirStateData AirStateData { get; private set; }
    [field: SerializeField] public MoveStateData MoveStateData { get; private set; }
    [field: SerializeField] public JumpStateData JumpStateData { get; private set; }
    [field: SerializeField] public DashStateData DashStateData { get; private set; }
    [field: SerializeField] public AnimationsData AnimationsData { get; private set; }
}
