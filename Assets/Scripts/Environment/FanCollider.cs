using UnityEngine;

public class FanCollider : MonoBehaviour
{
    private float _force;
    private ForceType _forceType;

    public void Initialize(float force, ForceType type)
    {
        _force = force;
        _forceType = type;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IForceControllable forceControllable))
        {
            forceControllable.ControlForce(transform.up * _force, _forceType);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IForceControllable forceControllable))
        {
            forceControllable.StopForceControl();
        }
    }
}
