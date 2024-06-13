
using UnityEngine;

/// <summary>
/// Defines context methods. Implements MonoBehaviour
/// </summary>
public abstract class AStateController: MonoBehaviour
{
    protected IState currentState;

    public abstract void Awake();

    public abstract void Start();

    public virtual void Update()
    {
        currentState.Update();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }

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
        currentState.Enter(this);

        Debug.Log(currentState.ToString());
    }
}
