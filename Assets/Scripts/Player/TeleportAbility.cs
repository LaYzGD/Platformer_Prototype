using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private GameObject _teleportObject;
    private bool _isTeleportObjectPlaced;

    public void Teleport()
    {
        if (_isTeleportObjectPlaced)
        {
            transform.position = _teleportObject.transform.position;
            _isTeleportObjectPlaced = false;
            _teleportObject.SetActive(false);
            return;
        }

        _teleportObject.transform.position = transform.position;
        _teleportObject.SetActive(true);
        _isTeleportObjectPlaced = true;
    }
}
