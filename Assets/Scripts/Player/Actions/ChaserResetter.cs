using System;
using UnityEngine;

/// <summary>
/// Provides reset ability of chaser position when colliding with a certain object.
/// </summary>
public class ChaserResetter
{
    public event EventHandler<float> ChaserResettedEvent;

    readonly float chaserResetDistance;

    //Sounds
    //[SerializeField] AudioSource policeSound;

    public ChaserResetter(float chaserResetDistance)
    {
        this.chaserResetDistance = chaserResetDistance;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Is triggered by a resetter object
        if (other.transform.CompareTag("Resetter"))
        {
            ChaserResettedEvent?.Invoke(this, chaserResetDistance);

            //policeSound.Play();//Calls police
        }
    }
}
