public class State
{
    protected StateMachine stateMachine { get; private set; }
    protected Player player { get; private set; }
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine; 
        player = this.stateMachine.Player;
    }

    public virtual void Enter() 
    {
        //UnityEngine.Debug.Log(GetType());
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}