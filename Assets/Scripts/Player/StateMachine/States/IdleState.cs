using UnityEngine;

public class IdleState : GroundedState
{
    private string _animationParameter;

    public IdleState(StateMachine stateMachine, string animationParameter) : base(stateMachine)
    {
        _animationParameter = animationParameter;
    }

    public override void Enter()
    {
        base.Enter();
        Rigidbody2D.velocity = new Vector2(0f, Rigidbody2D.velocity.y);
        player.PlayerAnimator.ChangeAnimationState(_animationParameter, true);
    }

    public override void Update()
    {
        base.Update();
        if (player.Inputs.HorizontalMovementDirection != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void Exit()
    {
        player.PlayerAnimator.ChangeAnimationState(_animationParameter, false);
    }
}
