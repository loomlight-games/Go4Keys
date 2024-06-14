using System;
using UnityEngine;

/// <summary>
/// Provides collecter behaviour of keys.
/// </summary>
public class KeyCollecter
{
    public event EventHandler AllFoundEvent;
    public event EventHandler<int> CollectibleFoundEvent;

    readonly int keysToCollect;
    int keysCollected = 0; // To restore from memory
    //KeySerializable keySerializable;

    public KeyCollecter(int keyToCollect)
    {
        this.keysToCollect = keyToCollect;
    }

    public void Initialize()
    {
        /*
        CollectibleFoundEvent?.Invoke(this, keysCollected);
        
        //Restores found counter from memory
        keySerializable = new KeySerializable();
        collectedCounter = keySerializable.DeserializeInt();

        //Debug.Log(collectedCounter);

        if (collectedCounter > -1)//At least one (0) collectible was found
            RestoreUI(collectedCounter);
        */
    }

    public void OnTriggerEnter(Collider other)
    {
        // A key
        if (other.gameObject.CompareTag("Key"))
        {
            //Deactivates it
            other.gameObject.SetActive(false);

            keysCollected++;

            CollectibleFoundEvent?.Invoke(this, keysCollected);

            if (keysCollected == keysToCollect)
                AllFoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }
} 