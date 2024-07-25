using UnityEngine;

public class InAirState : State
{
    private Checker _checker;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private Rigidbody2D _rigidBody2D;
    private AirStateData _data;
    private MoveStateData _moveData;
    private string _animationParameter;
    private bool _isJump;
    private Facing _facing;

    public InAirState(StateMachine stateMachine, AirStateData data, MoveStateData moveData, Facing facing, string animationParameter) : base(stateMachine)
    {
        _checker = player.Checker;
        _rigidBody2D = player.Rigidbody2D;
        _data = data;
        _animationParameter = animationParameter;
        _facing = facing;
        _moveData = moveData;
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();
        //playerAnimator.ChangeAnimationState(_animationParameter, true);
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

        if (player.Inputs.IsTeleport)
        {
            player.Inputs.UseTeleportInput();
            player.TeleportAbility.Teleport();
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
        float yVelocity = _rigidBody2D.velocity.y;
        float xVelocity = player.Inputs.HorizontalMovementDirection * _moveData.MovementSpeed;

        if (_isJump)
        {
            yVelocity -= _data.JumpDownwardVelocity;
            xVelocity = player.Inputs.HorizontalMovementDirection * _data.JumpHorizontalSpeed;
        }

        if (_rigidBody2D.velocity.y < 0)
        {
            _isJump = false;
            yVelocity -= _data.FallVelocity;
            if (yVelocity < _data.MaxFallVelocity * -1)
            {
                yVelocity = _data.MaxFallVelocity * -1;
            }
        }

        _rigidBody2D.velocity = new Vector2(player.IsHorizontalForceControlled ? _rigidBody2D.velocity.x : xVelocity, 
                                            player.IsVerticalForceControlled ? _rigidBody2D.velocity.y : yVelocity);
    }

    public void SetIsJumping() => _isJump = true;

    public override void Exit()
    {
        _isJump = false;
        //PlayerAnimator.ChangeAnimationState(_animationParameter, false);

    }
}
