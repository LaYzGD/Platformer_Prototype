using UnityEngine;

public class GroundedState : State
{
    private Checker _checker;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody2D;

    protected Rigidbody2D Rigidbody2D => _rigidbody2D;

    public GroundedState(StateMachine stateMachine) : base(stateMachine)
    {
        _checker = player.Checker;
        _rigidbody2D = player.Rigidbody2D;
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
            if (player.Inputs.HorizontalMovementDirection != player.Facing.FacingDirection && player.Inputs.HorizontalMovementDirection != 0)
            {
                player.Facing.Flip();
            }

            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.DashState.ResetCanDash();
        DoChecks();
    }

    public override void Update()
    {
        DoChecks();

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

        if (player.Inputs.IsThrow)
        {
            if (player.Inputs.VerticalMovementDirection != 0)
            {
                player.Thrower.ThrowLaser(new Vector2(0f, player.Inputs.VerticalMovementDirection), player.ThrowForce);
                player.Inputs.UseThrowInput();
                return;
            }

            player.Thrower.ThrowLaser(new Vector2(player.Facing.FacingDirection, 0f), player.ThrowForce);
            player.Inputs.UseThrowInput();
        }

        if (player.Inputs.IsJump)
        {
            player.Inputs.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }


        if (!_isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
