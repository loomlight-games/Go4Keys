
using UnityEngine;

/// <summary>
/// Defines context methods. Implements MonoBehaviour
/// </summary>
public abstract class AStateController: MonoBehaviour
{
    protected IState currentState;

    public abstract void Start();
    public abstract void Update();
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }

    /// <summary>
    /// Gets the current state of the controller
    /// </summary>
    /// <returns>Current state</returns>
    public virtual IState GetState()
    {
        return currentState;
    }
    /// <summary>
    /// Sets the current state of the controller
    /// </summary>
    /// <param name="state">New current state</param>
    public virtual void SetState(IState state)
    {
        currentState = state;
        Debug.Log(currentState.ToString());
        currentState.Enter(this);
    }
}
