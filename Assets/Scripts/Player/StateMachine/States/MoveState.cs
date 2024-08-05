using System;
using UnityEngine;

public class MoveState : GroundedState
{
    private PlayerData _data;
    private string _animationParameter;
    private Facing _facing;
    private Action<ParticleSystem> _createParticles;

    public MoveState(StateMachine stateMachine, PlayerData data, Facing facing, string animationParameter, Action<ParticleSystem> createParticles) : base(stateMachine)
    {
        _data = data;
        _facing = facing;
        _createParticles = createParticles;
        _animationParameter = animationParameter;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayerAnimator.OnAnimationTriggered += CreateStepEffects;
        player.PlayerAnimator.ChangeAnimationState(_animationParameter, true);
    }

    public override void Update()
    {
        base.Update();
        if (Rigidbody2D.velocity.x == 0 || player.Inputs.HorizontalMovementDirection == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.Inputs.HorizontalMovementDirection != _facing.FacingDirection && player.Inputs.HorizontalMovementDirection != 0)
        {
            _facing.Flip();
        }
    }

    private void CreateStepEffects()
    {
        _createParticles(player.DustParticles);
        player.AudioEffects.PlayRandomClip(_data.SoundData.StepClips);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var horizontalVelocity = player.Inputs.HorizontalMovementDirection * _data.MoveStateData.MovementSpeed;
        Rigidbody2D.velocity = new Vector2(player.IsHorizontalForceControlled ? player.Rigidbody2D.velocity.x + horizontalVelocity : horizontalVelocity, Rigidbody2D.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerAnimator.OnAnimationTriggered -= CreateStepEffects;
        player.PlayerAnimator.ChangeAnimationState(_animationParameter, false);
    }
}
