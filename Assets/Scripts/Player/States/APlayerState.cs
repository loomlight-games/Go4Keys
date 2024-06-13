using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : IState
{
    protected Player player;
    public abstract void Enter(AStateController controller);
    public virtual void Update() { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void Exit() { }
}