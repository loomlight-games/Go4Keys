using System;
using UnityEngine;

/// <summary>
/// Provides collecter behaviour of an specific object.
/// </summary>
public class Collecter
{
    public event EventHandler ObjectCollectedEvent;//Stores methods to invoke when hitting a collectible
    readonly GameObject collectible;

    //[SerializeField] AudioSource collectionSound;//Audio

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

            //Audio
            //collectionSound.Play();

            //Invokes methods in eventHandler
            ObjectCollectedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
} 