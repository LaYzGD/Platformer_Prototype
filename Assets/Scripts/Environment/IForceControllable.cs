using UnityEngine;
public interface IForceControllable
{
    public abstract void ControlForce(Vector2 force, ForceType type);
    public abstract void StopForceControl();
}

public enum ForceType 
{
    Horizontal,
    Vertical
}
