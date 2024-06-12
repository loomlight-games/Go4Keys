using UnityEngine;

/// <summary>
/// Implements common functionalities in all game states
/// </summary>
public abstract class AGameState : IState //MonoBehaviour, IState
{
    // Not mono behaviour because the controller handles each frame
    public abstract void FrameUpdate();
    public abstract void FrameFixedUpdate();

    public abstract void Enter();
    public abstract void Exit();
}
