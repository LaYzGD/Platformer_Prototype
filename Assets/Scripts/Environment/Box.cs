using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour, IForceControllable
{
    [SerializeField] private Rigidbody2D _rigidBody;

    private float _initialGravityScale;

    private void Start()
    {
        _initialGravityScale = _rigidBody.gravityScale;
    }

    public void ControlForce(Vector2 force, ForceType type)
    {
        _rigidBody.velocity = force;
    }

    public void StopForceControl()
    {
        _rigidBody.velocity = Vector2.zero;
    }

    public void ChangeGravity(float gravity, float time)
    {
        _rigidBody.gravityScale = gravity;
        StartCoroutine(RecoverGravity(time));
    }

    private IEnumerator RecoverGravity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _rigidBody.gravityScale = _initialGravityScale;
    }
}
