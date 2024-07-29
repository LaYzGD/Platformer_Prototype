using System.Collections;
using UnityEngine;

public class Door : ITriggerable
{
    [SerializeField] private GameObject _doorCollider;

    public override void Trigger()
    {
        _doorCollider?.SetActive(false);
    }

    public override void Trigger(float time)
    {
        _doorCollider?.SetActive(true);
        StartCoroutine(UnTriggerByTime(time));
    }

    public override void UnTrigger()
    {
        _doorCollider?.SetActive(true);
    }

    private IEnumerator UnTriggerByTime(float time) 
    {
        yield return new WaitForSecondsRealtime(time);
        _doorCollider?.SetActive(false);
    }
}
