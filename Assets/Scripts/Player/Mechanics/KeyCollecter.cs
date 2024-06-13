using System;
using UnityEngine;

/// <summary>
/// Provides collecter behaviour of keys.
/// </summary>
public class KeyCollecter
{
    public event EventHandler CollectibleFoundEvent;

    public void OnTriggerEnter(Collider other)
    {
        // A key
        if (other.gameObject.CompareTag("Key"))
        {
            //Deactivates it
            other.gameObject.SetActive(false);

            CollectibleFoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }
} 