using UnityEngine;
using System;
public class JumpState : AbilityState
{
    private Action<ParticleSystem> _createParticlesAction;
    private PlayerData _data;
    private int _amountOfJumps;
    public JumpState(StateMachine stateMachine, PlayerData data,  Action<ParticleSystem> createParticles) : base(stateMachine)
    {
        _data = data;
        _amountOfJumps = data.JumpStateData.AmountOfJumps;
        _createParticlesAction = createParticles;
    }

    public override void Enter()
    {
        base.Enter();
        player.UnGrab();
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _data.JumpStateData.JumpForce);
        player.Inputs.UseJumpInput();
        player.InAirState.SetIsJump();
        player.AudioEffects.PlaySound(_data.SoundData.JumpClip);
        _createParticlesAction(player.DustJumpParticles);
        _amountOfJumps--;
        IsAbilityDone = true;
    }

    public bool CanJump()
    {
        return _amountOfJumps > 0;
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumps = _data.JumpStateData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumps--;
}
