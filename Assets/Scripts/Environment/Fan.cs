using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private ForceType _forceType;
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
