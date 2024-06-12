using UnityEngine;

/// <summary>
/// Implements common functionalities in all game states
/// </summary>
public abstract class AGameState : IState
{
    public abstract void Enter(AStateController controller);

    // Not MonoBehaviour.Update() because the controller is already MonoBehaviour
    public abstract void FrameUpdate();

    public abstract void Exit();
}
