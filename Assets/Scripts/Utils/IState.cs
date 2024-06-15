using UnityEngine;

/// <summary>
/// Defines an state.
/// </summary>
public interface IState
{
    /// <summary>
    /// Called when entering an state.
    /// </summary>
    public void Enter(AStateController controller);

    /// <summary>
    /// Called when entering an state with additional information.
    /// </summary>
    public void Enter(AStateController controller, string info) { }

    /// <summary>
    /// Called in MonoBehaviour.Update() in controller.
    /// </summary>
    public void UpdateFrame();

    /// <summary>
    /// Called in MonoBehaviour.OnTriggerEnter(Collider other) in controller.
    /// </summary>
    public void OnTriggerEnter(Collider other);

    /// <summary>
    /// Called in MonoBehaviour.OnCollisionEnter(Collider other) in controller.
    /// </summary>
    public void OnCollisionEnter(Collision collision);

    /// <summary>
    /// Called in MonoBehaviour.Update() in controller to check state change.
    /// </summary>
    public void Exit();

}
