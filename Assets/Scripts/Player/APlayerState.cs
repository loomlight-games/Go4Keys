using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : IState
{
    public abstract void Enter(AStateController controller);
    public abstract void Exit();

    // Not mono behaviour because the controller handles each frame
    public abstract void FrameUpdate();
    public abstract void FrameFixedUpdate();
}
