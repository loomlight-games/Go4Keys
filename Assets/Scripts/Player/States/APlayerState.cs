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
    public virtual void Enter() { }
    public virtual void Enter(AStateController controller, string info)
    {
        player = (Player)controller;
        Enter(info);
    }
    public virtual void Enter(string info) { }
    public virtual void UpdateFrame()
    {
        Update();
        Exit();
    }
    public virtual void Update() { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void Exit() { }
}