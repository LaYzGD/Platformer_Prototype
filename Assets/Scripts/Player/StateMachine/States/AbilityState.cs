using UnityEngine;

public class AbilityState : State
{
    private float _startTime;
    private bool _isAbilityDone;
    private Rigidbody2D _rigidbody2D;
    protected bool IsAbilityDone { get => _isAbilityDone; set => _isAbilityDone = value; }
    protected float StartTime => _startTime;
    protected Rigidbody2D Rigidbody => _rigidbody2D;
    public AbilityState(StateMachine stateMachine) : base(stateMachine)
    {
        _rigidbody2D = player.Rigidbody2D;
    }

    public override void Enter()
    {
        base.Enter();
        _isAbilityDone = false;
        _startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (_isAbilityDone)
        {
            if (player.Checker.IsGrounded())
            {
                stateMachine.ChangeState(player.IdleState);
                return;
            }

            stateMachine.ChangeState(player.InAirState);
        }
    }
}
