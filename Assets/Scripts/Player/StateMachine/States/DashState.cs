using UnityEngine;
using System;

public class DashState : AbilityState
{
    public bool CanDash { get; private set; }

    private float _startTime;
    private float _lastDashTime;
    private float _startGravityScale;
    private DashStateData _data;
    //private ParticleSystem[] _dashParticles;
    //private Action _createParticlesAction;

    public DashState(StateMachine stateMachine, DashStateData data/*, ParticleSystem[] dashParticles, Action createParticles*/) : base(stateMachine)
    {
        _data = data;
        //_dashParticles = dashParticles;
        //_createParticlesAction = createParticles;
    }

    public override void Enter()
    {
        base.Enter();
        CanDash = false;
        //player.PlayerAnimator.ChangeAnimationState(_data.DashAnimationParameter, true);
        player.Inputs.UseDashInput();
        //foreach (var particle in _dashParticles)
        //{
        //    particle.transform.localScale = new Vector3(player.Facing.FacingDirection, particle.transform.localScale.y, particle.transform.localScale.z);
        //    particle.Play();
        //}
        _startTime = Time.time;

        //if (player.Checker.IsGrounded())
        //{
        //    _createParticlesAction();
        //}

        //player.Sounds.PlayAbilitySound(_data.DashSound);
        _startGravityScale = player.Rigidbody2D.gravityScale;
        player.Rigidbody2D.gravityScale = 0f;
    }

    public override void Update()
    {
        if (Time.time >= _startTime + _data.DashTime)
        {
            player.Rigidbody2D.gravityScale = _startGravityScale;
            IsAbilityDone = true;
            _lastDashTime = Time.time;
        }

        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.Rigidbody2D.velocity = new Vector2(_data.DashVelocity * player.Facing.FacingDirection, player.IsVerticalForceControlled ? player.Rigidbody2D.velocity.y : 0);
    }

    public override void Exit()
    {
        base.Exit();
        player.Rigidbody2D.gravityScale = _startGravityScale;
        IsAbilityDone = true;
        _lastDashTime = Time.time;
        //player.PlayerAnimator.ChangeAnimationState(_data.DashAnimationParameter, false);
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + _data.DashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
