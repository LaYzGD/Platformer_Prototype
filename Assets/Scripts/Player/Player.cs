using UnityEngine;

public class Player : MonoBehaviour, IForceControllable
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private HingeJoint2D _joint2D;
    [SerializeField] private int _defaultFacingDirection = 1;
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public PlayerAnimator PlayerAnimator { get; private set; }
    [field: SerializeField] public Inputs Inputs { get; private set; }
    [field: SerializeField] public TeleportAbility TeleportAbility { get; private set; }
    [field: SerializeField] public GravitationAbility GravitationAbility { get; private set; }
    [field: SerializeField] public StickyLaserThrower Thrower { get; private set; }
    public Facing Facing { get; private set; }
    public Checker Checker { get; private set; }

    private StateMachine _stateMachine;
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public InAirState InAirState { get; private set; }
    public JumpState JumpState { get; private set; }
    public DashState DashState { get; private set; }

    public bool IsHorizontalForceControlled { get; private set; }
    public bool IsVerticalForceControlled { get; private set; }
    public bool IsGrabbed { get; private set; }
    public float ThrowForce { get; private set; }

    private void Awake()
    {
        _stateMachine = new StateMachine(this);
        Facing = new Facing(transform, _defaultFacingDirection);
        Checker = new Checker(_collider2D, _playerData.GroundCheckData, Rigidbody2D);
        IdleState = new IdleState(_stateMachine, _playerData.AnimationsData.IdleAnimationParameter);
        MoveState = new MoveState(_stateMachine, _playerData.MoveStateData, Facing, _playerData.AnimationsData.MoveAnimationParameter);
        InAirState = new InAirState(_stateMachine, Facing, _playerData);
        JumpState = new JumpState(_stateMachine, _playerData.JumpStateData);
        DashState = new DashState(_stateMachine, _playerData.DashStateData);
        ThrowForce = _playerData.ThrowForce;
    }

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (Inputs.IsInteract && IsGrabbed)
        {
            UnGrab();
        }

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void ControlForce(Vector2 force, ForceType type)
    {
        switch (type) 
        {
            case ForceType.Horizontal:
                IsHorizontalForceControlled = true;
                Rigidbody2D.velocity = new Vector2(force.x, Rigidbody2D.velocity.y);
                break;
            case ForceType.Vertical:
                IsVerticalForceControlled = true;
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, force.y);
                break;
        }
    }

    public void StopForceControl() 
    {
        Rigidbody2D.velocity = Vector2.zero;
        IsHorizontalForceControlled = false;
        IsVerticalForceControlled = false;
    }

    public void Grab(Rigidbody2D rigidBody)
    {
        _joint2D.connectedBody = rigidBody;
        _joint2D.enabled = true;
        IsGrabbed = true;
    }

    public void UnGrab()
    {
        _joint2D.connectedBody = null;
        _joint2D.enabled = false;
        IsGrabbed = false;
    }
}
