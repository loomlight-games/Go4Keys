using System;
using UnityEngine;

//HANDLES KEY COUNT AUTOSAVE (SERIALIZATION)

[Serializable]
public class KeyAutosave
{
    readonly KeySerializable keySerializable = new();

    //Dirty flag boolean
    bool dirtyKey = false;

    //Key counter
    int count = -1;//None

    //Key count resetter (main menu button and victory)
    //KeyCountResetter resetter;

    public void Start()
    {
        //SUBSCRIBES INCREMENT COUNT TO EVENT HANDLER OF COLLECTER
        Player.Instance.keyCollecter.CollectibleFoundEvent += UpdateCount;
        Player.Instance.keyCollecter.AllFoundEvent += ResetAtVictory;

        //SUBSCRIBES RESET COUNT TO EVENT HANDLER OF MAIN MENU BUTTON
        //resetter.KeyCountResetEvent += ResetFromResetter;

        //Restores found counter from memory
        count = keySerializable.Deserialize("keyCount.json");
    }

    public void Update()
    {
        if(dirtyKey)
        {
            //Saves in memory key count
            keySerializable.Serialize(count, "keyCount.json");

            //Cleans after saving
            dirtyKey = false;
        }
    }

    void ResetFromResetter(object sender, EventArgs e)
    {
        count = -1;
        dirtyKey = true;
    }

    void ResetAtVictory(object sender, EventArgs e)
    {
        count = -2;
        dirtyKey = true;
        
    }

    void UpdateCount(object sender, int collected)
    {
        count = collected;
        dirtyKey = true;
    }
}
