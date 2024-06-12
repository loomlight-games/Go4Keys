
/// <summary>
/// Defines an state
/// </summary>
public interface IState
{
    /// <summary>
    /// Called when entering an state
    /// </summary>
    /// 
    public void Enter();

    /// <summary>
    /// Called when exiting an state
    /// </summary>
    public void Exit();

}
