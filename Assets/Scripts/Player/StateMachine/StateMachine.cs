public class StateMachine
{
    public Player Player { get; private set; }
    private State _currentState;
    
    public StateMachine(Player player)
    {
        Player = player;
    }

    public void Initialize(State startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
