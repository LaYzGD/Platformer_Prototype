using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] private float _jumpInputHoldTime = 0.2f;
    [SerializeField] private float _dashInputHoldTime = 0.2f;
    [SerializeField] private float _throwInputHoldTime = 0.2f;
    [SerializeField] private float _teleportInputHoldTime = 0.2f;
    [SerializeField] private float _gravityInputHoldTime = 0.2f;
    [SerializeField] private GameObject _settingsView;
    public int VerticalMovementDirection { get; private set; }
    public int HorizontalMovementDirection { get; private set; }
    public bool IsThrow { get; private set; }
    public bool IsTeleport { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsDash { get; private set; }
    public bool IsGravity { get; private set; }

    private float _jumpStartTime;
    private float _dashInputStartTime;
    private float _throwInputStartTime;
    private float _teleportInputStartTime;
    private float _gravityInputStartTime;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpStartTime + _jumpInputHoldTime)
        {
            IsJump = false;
        }
    }

    private void CheckDashInput()
    {
        if (Time.time >= _dashInputStartTime + _dashInputHoldTime)
        {
            IsDash = false;
        }
    }

    private void CheckThrowInputHoldTime()
    {
        if (Time.time >= _throwInputStartTime + _throwInputHoldTime)
        {
            IsThrow = false;
        }
    }

    private void CheckTeleportInputHoldTime()
    {
        if (Time.time >= _teleportInputStartTime + _teleportInputHoldTime)
        {
            IsTeleport = false;
        }
    }

    private void CheckGravityInputHoldTime()
    {
        if (Time.time >= _gravityInputStartTime + _gravityInputHoldTime)
        {
            IsGravity = false;
        }
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInput();
        CheckThrowInputHoldTime();
        CheckTeleportInputHoldTime();
        CheckGravityInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        HorizontalMovementDirection = Mathf.RoundToInt(direction.x);
        VerticalMovementDirection = Mathf.RoundToInt(direction.y);
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _settingsView.SetActive(true);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsJump = true;
            _jumpStartTime = Time.time;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsDash = true;
            _dashInputStartTime = Time.time;
        }
    }

    public void OnThrowInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsThrow = true;
            _throwInputStartTime = Time.time;
        }
    }

    public void OnTeleportInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsTeleport = true;
            _teleportInputStartTime = Time.time;
        }
    }

    public void OnGravityInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsGravity = true;
            _gravityInputStartTime = Time.time;
        }
    }

    public void UseTeleportInput() => IsTeleport = false;
    public void UseGravityInput() => IsGravity = false;

    public void UseThrowInput() => IsThrow = false;

    public void UseJumpInput() => IsJump = false;

    public void UseDashInput() => IsDash = false;
}
