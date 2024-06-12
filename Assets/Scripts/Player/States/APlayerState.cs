using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class APlayerState : IState
{
    public abstract void Enter(AStateController controller);

    // Not MonoBehaviour.Update() because the controller is already MonoBehaviour
    public abstract void FrameUpdate();

    public abstract void Exit();
}
