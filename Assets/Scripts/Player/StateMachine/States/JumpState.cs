using UnityEngine;
using System;
public class JumpState : AbilityState
{
    private Action<ParticleSystem> _createParticlesAction;
    private JumpStateData _data;
    public JumpState(StateMachine stateMachine, JumpStateData data,  Action<ParticleSystem> createParticles) : base(stateMachine)
    {
        _data = data;
        _createParticlesAction = createParticles;
    }

    public override void Enter()
    {
        base.Enter();
        player.UnGrab();
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _data.JumpForce);
        player.Inputs.UseJumpInput();
        player.InAirState.SetIsJumping();
        //player.Sounds.PlayAbilitySound(_data.JumpSound);
        _createParticlesAction(player.DustJumpParticles);
        IsAbilityDone = true;
    }
}
