using System;
using UnityEngine;

//HANDLES KEY COUNT AUTOSAVE (SERIALIZATION)

[Serializable]
public class KeyAutosave
{
    //Dirty flag boolean
    bool dirtyKey = false;

    //Key counter
    int keyCount = -1;//None

    //Serializable key object
    KeySerializable key;

    //Key count resetter (main menu button and victory)
    //KeyCountResetter resetter;

    public void Start()
    {
        //SUBSCRIBES INCREMENT COUNT TO EVENT HANDLER OF COLLECTER
        Player.Instance.keyCollecter.CollectibleFoundEvent += IncrCount;

        //SUBSCRIBES RESET COUNT TO EVENT HANDLER OF MAIN MENU BUTTON
        //resetter.KeyCountResetEvent += ResetFromResetter;

        //SUBSCRIBES RESET COUNT TO EVENT HANDLER OF VICTORY
        GameManager.Instance.playerCollectedUI.AllFoundEvent += ResetAtVictory;

        //Restores found counter from memory
        KeySerializable keySerializable = new ();
        keyCount = keySerializable.DeserializeInt();
    }

    public void Update()
    {
        if(dirtyKey)
        {
            //Creates serializable object
            key = new KeySerializable(keyCount);

            //Saves in memory key count
            key.Serialize();

            //Cleans after saving
            dirtyKey = false;
        }
    }

    void ResetFromResetter(object sender, EventArgs e)
    {
        keyCount = -1;
        dirtyKey = true;
    }

    void ResetAtVictory(object sender, EventArgs e)
    {
        keyCount = -2;
        dirtyKey = true;
    }

    void IncrCount(object sender, EventArgs e)
    {
        keyCount++;
        dirtyKey = true;
    }
}
