using System;
using UnityEngine;

/// <summary>
/// Provides collecter behaviour of an specific object.
/// </summary>
public class Collecter
{
    public event EventHandler CollectibleFoundEvent;
    
    readonly GameObject collectible;

    public Collecter(GameObject collectible)
    {
        this.collectible = collectible;
    }

    //Collision trigger
    public void OnTriggerEnter(Collider other)
    {
        //With an object with the same tag of the one to collect
        if (other.gameObject.CompareTag(collectible.tag))
        {
            //Deactivates it
            other.gameObject.SetActive(false);

            CollectibleFoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }
} 