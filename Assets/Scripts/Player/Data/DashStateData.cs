using UnityEngine;


[CreateAssetMenu(menuName = "PlayerData/StateData/DashState", fileName = "DashStateData")]
public class DashStateData : ScriptableObject
{
    [field: SerializeField] public float DashVelocity { get; private set; }
    [field: SerializeField] public float DashCooldown { get; private set; }
    [field: SerializeField] public float DashTime { get; private set; }
    [field: SerializeField] public string DashAnimationParameter { get; private set; }
}
