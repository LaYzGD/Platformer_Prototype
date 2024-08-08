using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/StateData/JumpState", fileName = "JumpStateData")]
public class JumpStateData : ScriptableObject
{
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float JumpGrabForceX { get; private set; }
    [field: SerializeField] public float JumpGrabForceY { get; private set; }
    [field: SerializeField] public int AmountOfJumps { get; private set; }
}
