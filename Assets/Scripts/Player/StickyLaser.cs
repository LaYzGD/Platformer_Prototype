using UnityEngine;
using System;

public class StickyLaser : MonoBehaviour
{
    [SerializeField] private Collider2D _collider; 
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioSource _audioSource;

    private float _actiovationRange;
    private float _actiovationTime;

    private Action<StickyLaser> _onPlacedAction;

    public void Initialize(Action<StickyLaser> onPlacedAction, Vector2 position, Vector2 velocity, Transform parent, float activationRange, float actiovationTime) 
    {
        transform.SetParent(parent);
        _onPlacedAction = onPlacedAction;
        transform.position = position;
        _rigidBody2D.isKinematic = false;
        _collider.enabled = true;
        gameObject.SetActive(true);
        _actiovationRange = activationRange;
        _actiovationTime = actiovationTime;
        SetVelocity(velocity);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rigidBody2D.velocity = velocity;
    }

    public void Trigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _actiovationRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out ITriggerable triggerable))
            {
                triggerable.Trigger(_actiovationTime);
            }
        }

        Destroy();
    }

    public void Destroy()
    {
        _particles.gameObject.transform.position = transform.position;
        _particles.Play();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_groundLayer.value, 2))
        {
            _rigidBody2D.velocity = Vector2.zero;
            _rigidBody2D.isKinematic = true;
            _audioSource.Play();
            _collider.enabled = false;
            transform.SetParent(collision.transform);
            _onPlacedAction(this);
        }
    }
}
