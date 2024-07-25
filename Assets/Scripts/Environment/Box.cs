using UnityEngine;

public class Box : MonoBehaviour, IForceControllable
{
    [SerializeField] private Rigidbody2D _rigidBody;
    public void ControlForce(Vector2 force, ForceType type)
    {
        _rigidBody.velocity = force;
    }

    public void StopForceControl()
    {
        _rigidBody.velocity = Vector2.zero;
    }
}
