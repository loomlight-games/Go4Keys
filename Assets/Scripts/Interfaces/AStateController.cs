
using UnityEngine;

/// <summary>
/// Defines context methods
/// </summary>
public abstract class AStateController: MonoBehaviour
{
    public abstract void Start();
    public abstract void Update();

    /// <summary>
    /// Gets the current state of the controller
    /// </summary>
    /// <returns>Current state</returns>
    public abstract IState GetState();
    /// <summary>
    /// Sets the current state of the controller
    /// </summary>
    /// <param name="state">New current state</param>
    public abstract void SetState(IState state);
}
