using UnityEngine;

public class GravitationAbility : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _gravity;
    [SerializeField] private float _time;

    public void UseAbility()
    {
        var coliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (var col in coliders)
        {
            if (col.TryGetComponent(out IForceControllable controllable))
            {
                controllable?.ChangeGravity(_gravity, _time);
            }
        }
    }
}
