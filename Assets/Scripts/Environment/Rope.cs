using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody2D;
    private bool _isGrabbed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isGrabbed)
        {
            return;
        }

        if (collision.TryGetComponent(out Player player))
        {
            if (player.Inputs.IsInteract)
            {
                player.Inputs.UseInteractInput();
                player.Grab(_rigidBody2D);
                _isGrabbed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isGrabbed = false;
        }
    }
}
