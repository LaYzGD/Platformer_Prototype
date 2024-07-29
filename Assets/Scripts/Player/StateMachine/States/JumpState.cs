using UnityEngine;
using System;
public class JumpState : AbilityState
{
    //private Action _createParticlesAction;
    //private ParticleSystem[] _jumpParticles;
    private JumpStateData _data;
    public JumpState(StateMachine stateMachine, JumpStateData data/*, ParticleSystem[] jumpParticles, Action createParticles*/) : base(stateMachine)
    {
        _data = data;
        //_jumpParticles = jumpParticles;
        //_createParticlesAction = createParticles;
    }

    public override void Enter()
    {
        base.Enter();
        player.UnGrab();
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _data.JumpForce);
        player.Inputs.UseJumpInput();
        player.InAirState.SetIsJumping();
        //player.Sounds.PlayAbilitySound(_data.JumpSound);
        //foreach (var particle in _jumpParticles)
        //{
        //    particle.Play();
        //}
        //_createParticlesAction();
        IsAbilityDone = true;
    }
}
