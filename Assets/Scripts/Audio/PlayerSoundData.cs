using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/SoundData", fileName = "SoundData")]
public class PlayerSoundData : ScriptableObject
{
    [field: SerializeField] public AudioClip[] StepClips { get; private set; }
    [field: SerializeField] public AudioClip JumpClip { get; private set; }
    [field: SerializeField] public AudioClip DashClip { get; private set; }
    [field: SerializeField] public AudioClip TeleportClip { get; private set; }
    [field: SerializeField] public AudioClip TeleportPlaceClip { get; private set; }
    [field: SerializeField] public AudioClip GravicyChangeClip { get; private set; }
}
