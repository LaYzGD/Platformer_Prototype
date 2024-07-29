using UnityEngine;

public abstract class ITriggerable: MonoBehaviour
{
    public abstract void Trigger();
    public abstract void Trigger(float time);
    public abstract void UnTrigger();
}
