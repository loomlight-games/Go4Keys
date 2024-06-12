using System;
using UnityEngine;

//HANDLES COLLECTION OF AN OBJECT

public class Collecter : MonoBehaviour
{
    //EVENT HANDLER FOR COLLECTING
    public event EventHandler ObjectCollectedEvent;//Stores methods to invoke when hitting a collectible
    
    //Object to collect
    [SerializeField] GameObject collectible;

    //Audio
    [SerializeField] AudioSource collectionSound;

    //Collision trigger
    private void OnTriggerEnter(Collider other)
    {
        //With an object with the same tag of the one to collect
        if (other.gameObject.CompareTag(collectible.tag))
        {
            //Destroys it
            Destroy(other.gameObject);

            //Audio
            collectionSound.Play();

            //Invokes methods in eventHandler
            ObjectCollectedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// when hitting a collectible
///     destroy it
///     play sound
///     invoke methods in event handler
///     