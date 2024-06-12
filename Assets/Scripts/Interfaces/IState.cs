
/// <summary>
/// Defines an state
/// </summary>
public interface IState
{
    /// <summary>
    /// Called when entering an state
    /// </summary>
    /// 
    public void Enter(AStateController controller);

    /// <summary>
    /// Called in MonoBehaviour.Update() in controller
    /// </summary>
    public void FrameUpdate();

    /// <summary>
    /// Called in MonoBehaviour.FixedUpdate() in controller
    /// </summary>
    public virtual void FrameFixedUpdate() { } // Virtual = optional

    /// <summary>
    /// Called when exiting an state
    /// </summary>
    public void Exit();

}
