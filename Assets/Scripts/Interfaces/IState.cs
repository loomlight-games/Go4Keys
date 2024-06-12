
using UnityEngine;

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
    public void Update();

    /// <summary>
    /// Called in MonoBehaviour.OnTriggerEnter(Collider other) in controller
    /// </summary>
    /// <param name="other">Trigger collided</param>
    public void OnTriggerEnter(Collider other);

    /// <summary>
    /// Called in MonoBehaviour.OnTriggerExit(Collider other) in controller
    /// </summary>
    /// <param name="other">Trigger collided</param>
    public void OnTriggerExit(Collider other);

    /// <summary>
    /// Called when exiting an state
    /// </summary>
    public void Exit();

}
