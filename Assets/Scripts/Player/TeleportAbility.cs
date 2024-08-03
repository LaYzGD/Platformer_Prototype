using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private GameObject _teleportObject;
    [SerializeField] private ParticleSystem _teleportParticles;
    [SerializeField] private ParticleSystem _teleportFromParticles;
    private bool _isTeleportObjectPlaced;

    public void Teleport()
    {
        if (_isTeleportObjectPlaced)
        {
            _teleportFromParticles.transform.position = transform.position;
            _teleportFromParticles.Play();
            transform.position = _teleportObject.transform.position;
            _isTeleportObjectPlaced = false;
            _teleportObject.SetActive(false);
            _teleportParticles.Play();
            return;
        }

        _teleportObject.transform.position = transform.position;
        _teleportObject.SetActive(true);
        _isTeleportObjectPlaced = true;
    }
}
