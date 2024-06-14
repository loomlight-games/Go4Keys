using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : IState
{
    protected Player player;
    public virtual void Enter(AStateController controller)
    {
        player = (Player)controller;

        Enter();
    }
    public abstract void Enter();
    public virtual void Update() { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void Exit() { }
}