using UnityEngine;

/// <summary>
/// Implements common functionalities in all player states
/// </summary>
public abstract class AState
{
    public virtual void Enter() { }
    public virtual void Enter(string info) { }
    public virtual void Update() { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void Exit() { }
}