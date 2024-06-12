using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : IState
{
    protected Player player;
    public abstract void Enter(AStateController controller);
    public abstract void Update();
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    public abstract void Exit();

}