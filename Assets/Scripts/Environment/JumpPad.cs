using UnityEngine;

public class JumpPad : ITriggerable
{
    [SerializeField] private GameObject _jumpPadGraphics;
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isOn;

    private void Start()
    {
        _jumpPadGraphics.SetActive(_isOn);
        _collider2D.enabled = _isOn;
    }

    public override void Trigger()
    {
        _isOn = !_isOn;
        _jumpPadGraphics.SetActive(_isOn);
        _collider2D.enabled = _isOn;
    }

    public override void UnTrigger()
    {
        _isOn = !_isOn;
        _jumpPadGraphics.SetActive(_isOn);
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
            rigidbody2D.AddForce(transform.up * _jumpForce * rigidbody2D.mass, ForceMode2D.Impulse);
        }
    }
}
