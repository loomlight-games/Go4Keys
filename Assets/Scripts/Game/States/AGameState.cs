using UnityEngine;

/// <summary>
/// Implements common functionalities in all game states
/// </summary>
public abstract class AGameState : IState //MonoBehaviour, IState
{
    protected GameManager game;

    public virtual void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        Enter();
    }
    public abstract void Enter();
    public virtual void Update() { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public abstract void Exit();

}
