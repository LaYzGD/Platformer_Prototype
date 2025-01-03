using System.Collections;
using UnityEngine;

public class JumpPad : ITriggerable
{
    [SerializeField] private string _triggerParameter = "activate";
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isOn;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _collider2D.enabled = _isOn;
    }

    public override void Trigger()
    {
        _isOn = !_isOn;
        _collider2D.enabled = _isOn;
    }

    public override void UnTrigger()
    {
        _isOn = !_isOn;
        _collider2D.enabled = _isOn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isOn)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.InAirState.SetIsJumpPad();
            }
            _audioSource.Play();
            _animator.SetTrigger(_triggerParameter);
            _jumpParticles.Play();
            rigidbody2D.AddForce(transform.up * _jumpForce * rigidbody2D.mass, ForceMode2D.Impulse);
        }
    }

    public override void Trigger(float time)
    {
        Trigger();
        StartCoroutine(UnTriggerByTime(time));
    }

    private IEnumerator UnTriggerByTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        UnTrigger();
    }
}
