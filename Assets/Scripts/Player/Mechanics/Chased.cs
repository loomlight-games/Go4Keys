using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Provides reset ability of chaser position when colliding with a certain object.
/// Can be caught if collides with chaser as well.
/// </summary>
public class Chased
{
    public event EventHandler<float> ResetChaserEvent;
    public event EventHandler CaughtEvent;

    readonly float chaserResetDistance;

    public Chased(float chaserResetDistance)
    {
        this.chaserResetDistance = chaserResetDistance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Resetter"))
            ResetChaserEvent?.Invoke(this, chaserResetDistance);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            CaughtEvent?.Invoke(this, EventArgs.Empty);
    }
}
