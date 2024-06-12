using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : MonoBehaviour, IState
{
    public abstract void Update();
    public abstract void FixedUpdate();

    public abstract void Enter();
    public abstract void Exit();
}
