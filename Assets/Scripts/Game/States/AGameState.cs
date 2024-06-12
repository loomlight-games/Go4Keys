using UnityEngine;

/// <summary>
/// Implements common functionalities in all game states
/// </summary>
public abstract class AGameState : IState //MonoBehaviour, IState
{
    public abstract void Enter(AStateController controller);
    public abstract void Update();
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    public abstract void Exit();

}
