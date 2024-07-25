using UnityEngine;

public class Door : ITriggerable
{
    [SerializeField] private GameObject _doorCollider;

    public override void Trigger()
    {
        _doorCollider?.SetActive(false);
    }

    public override void UnTrigger()
    {
        _doorCollider?.SetActive(true);
    }
}
