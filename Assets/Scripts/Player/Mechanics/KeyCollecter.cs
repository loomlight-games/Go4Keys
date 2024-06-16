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
    int keysCollected = 0;

    public KeyCollecter(int keyToCollect)
    {
        this.keysToCollect = keyToCollect;
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