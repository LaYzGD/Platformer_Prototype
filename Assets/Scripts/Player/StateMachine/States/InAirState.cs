using UnityEngine;

public class InAirState : State
{
    private Checker _checker;
    private bool _isGrounded;
    private Rigidbody2D _rigidBody2D;
    private AirStateData _data;
    private MoveStateData _moveData;
    private string _inAirAnimationParameter;
    private bool _isJumpPad;
    private Facing _facing;
    private float _swingSpeed;

    private float _coyoteeTimeStart;
    private float _coyoteeTime;

    public InAirState(StateMachine stateMachine, Facing facing, PlayerData data) : base(stateMachine)
    {
        _checker = player.Checker;
        _rigidBody2D = player.Rigidbody2D;
        _data = data.AirStateData;
        _inAirAnimationParameter = data.AnimationsData.InAirAnimationParameter;
        _facing = facing;
        _moveData = data.MoveStateData;
        _coyoteeTime = data.AirStateData.CoyoteeTime;
        _swingSpeed = data.SwingSpeed;
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();
        _coyoteeTimeStart = Time.time;
        player.PlayerAnimator.ChangeAnimationState(_inAirAnimationParameter, true);
    }

    public void DoChecks()
    {
        _isGrounded = _checker.IsGrounded();
        if (player.Inputs.IsDash)
        {
            if (!player.DashState.CheckIfCanDash())
            {
                return;
            }
            if (player.Inputs.HorizontalMovementDirection != _facing.FacingDirection && player.Inputs.HorizontalMovementDirection != 0)
            {
                _facing.Flip();
            }

            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void Update()
    {
        DoChecks();

        if (player.Inputs.IsJump && !player.IsGrabbed)
        {
            player.Inputs.UseJumpInput();
            if (Time.time < _coyoteeTime + _coyoteeTimeStart)
            {
                stateMachine.ChangeState(player.JumpState);
            }
        }

        if (player.Inputs.IsTeleport)
        {
            player.Inputs.UseTeleportInput();
            player.TeleportAbility.Teleport();
        }

        if (player.Inputs.IsGravity)
        {
            player.Inputs.UseGravityInput();
            player.GravitationAbility.UseAbility();
        }

        if (player.IsGrabbed)
        {
            if (player.Inputs.IsJump)
            {
                player.Inputs.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }

        if (_isGrounded)
        {
           stateMachine.ChangeState(player.IdleState);
        }

        if (player.Inputs.HorizontalMovementDirection != _facing.FacingDirection && player.Inputs.HorizontalMovementDirection != 0)
        {
            _facing.Flip();
        }
    }

    public override void FixedUpdate()
    {
        float yVelocity = _rigidBody2D.velocity.y - _data.JumpDownwardVelocity;
        float xVelocity = player.Inputs.HorizontalMovementDirection * (_moveData.MovementSpeed + (player.IsGrabbed ? _swingSpeed : 0f));


        if (_isJumpPad)
        {
            xVelocity = _rigidBody2D.velocity.x;
        }

        if (_rigidBody2D.velocity.y < 0)
        {
            _isJumpPad = false;
            yVelocity -= _data.FallVelocity;
            if (yVelocity < _data.MaxFallVelocity * -1)
            {
                yVelocity = _data.MaxFallVelocity * -1;
            }
        }

        _rigidBody2D.velocity = new Vector2(player.IsHorizontalForceControlled ? _rigidBody2D.velocity.x + xVelocity : xVelocity, 
                                            player.IsVerticalForceControlled ? _rigidBody2D.velocity.y : yVelocity);
    }

    public void SetIsJumpPad() => _isJumpPad = true;

    public override void Exit()
    {
        _isJumpPad = false;
        player.PlayerAnimator.ChangeAnimationState(_inAirAnimationParameter, false);
    }
}
