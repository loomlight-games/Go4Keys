using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Provides reset ability of chaser position when colliding with a certain object.
/// Can be caught if collides with chaser as well.
/// </summary>
public class ChaserResetter
{
    public event EventHandler<float> ChaserResettedEvent;
    public event EventHandler CaughtEvent;

    readonly float chaserResetDistance;

    public ChaserResetter(float chaserResetDistance)
    {
        this.chaserResetDistance = chaserResetDistance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Resetter"))
            ChaserResettedEvent?.Invoke(this, chaserResetDistance);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            CaughtEvent?.Invoke(this, EventArgs.Empty);
    }
}
