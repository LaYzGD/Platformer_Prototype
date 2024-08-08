using UnityEngine;

public class Checker : MonoBehaviour
{
    private GroundCheckData _data;
    private LayerMask _groundLayer;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;

    public void Init(GroundCheckData data, Rigidbody2D rb)
    {
        _data = data;
        _groundLayer = _data.GroundLayer;
        _rigidbody2D = rb;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_groundLayer.value, 2))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_groundLayer.value, 2))
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return _isGrounded && _rigidbody2D.velocity.y < _data.VerticalVelocityTreshold;
    }
}
