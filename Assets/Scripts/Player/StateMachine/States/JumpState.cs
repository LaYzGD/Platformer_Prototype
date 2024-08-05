using UnityEngine;
using System;
public class JumpState : AbilityState
{
    private Action<ParticleSystem> _createParticlesAction;
    private PlayerData _data;
    public JumpState(StateMachine stateMachine, PlayerData data,  Action<ParticleSystem> createParticles) : base(stateMachine)
    {
        _data = data;
        _createParticlesAction = createParticles;
    }

    public override void Enter()
    {
        base.Enter();
        player.UnGrab();
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _data.JumpStateData.JumpForce);
        player.Inputs.UseJumpInput();
        player.AudioEffects.PlaySound(_data.SoundData.JumpClip);
        _createParticlesAction(player.DustJumpParticles);
        IsAbilityDone = true;
    }
}
