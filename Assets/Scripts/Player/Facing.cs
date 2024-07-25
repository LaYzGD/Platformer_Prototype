using UnityEngine;

public class Facing
{
    private Transform _body;
    private int _facingDirection;

    public int FacingDirection => _facingDirection;

    public Facing(Transform body, int standartFacingDirection)
    {
        _body = body;
        _facingDirection = standartFacingDirection;
    }

    public void Flip()
    {
        _body.Rotate(0, 180f, 0);
        _facingDirection *= -1;
    }
}
