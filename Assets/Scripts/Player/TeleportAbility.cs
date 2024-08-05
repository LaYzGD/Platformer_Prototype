using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private GameObject _teleportObject;
    [SerializeField] private ParticleSystem _teleportParticles;
    [SerializeField] private ParticleSystem _teleportFromParticles;
    private bool _isTeleportObjectPlaced;

    private Player _player;
    private PlayerData _data;

    public void InitData(Player player, PlayerData data)
    {
        _player = player;
        _data = data;
    }

    public void Teleport()
    {
        if (_isTeleportObjectPlaced)
        {
            _teleportFromParticles.transform.position = transform.position;
            _teleportFromParticles.Play();
            transform.position = _teleportObject.transform.position;
            _isTeleportObjectPlaced = false;
            _player.AudioEffects.PlaySound(_data.SoundData.TeleportClip);
            _teleportObject.SetActive(false);
            _teleportParticles.Play();
            return;
        }

        _player.AudioEffects.PlaySound(_data.SoundData.TeleportPlaceClip);
        _teleportObject.transform.position = transform.position;
        _teleportObject.SetActive(true);
        _isTeleportObjectPlaced = true;
    }
}
