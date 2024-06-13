using System;
using UnityEngine;

//HANDLES KEY COUNT AUTOSAVE (SERIALIZATION)

[Serializable]
public class KeyAutosave : MonoBehaviour
{
    //Dirty flag boolean
    bool dirtyKey = false;

    //Key counter
    int keyCount = -1;//None

    //Serializable key object
    KeySerializable key;

    //Subject (key collecter)
    [SerializeField] KeyCollecter collecter;

    //Key count resetter (main menu button and victory)
    [SerializeField] KeyCountResetter resetter;
    [SerializeField] CollectiblesUI collectiblesUI;

    private void ResetFromResetter(object sender, EventArgs e)
    {
        keyCount = -1;
        dirtyKey = true;
    }

    private void ResetAtVictory(object sender, EventArgs e)
    {
        keyCount = -2;
        dirtyKey = true;
    }


    private void IncrCount(object sender, EventArgs e)
    {
        keyCount++;
        dirtyKey = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        collecter = Player.Instance.keyCollecter;
        //SUBSCRIBES INCREMENT COUNT TO EVENT HANDLER OF COLLECTER
        collecter.CollectibleFoundEvent += IncrCount;

        //SUBSCRIBES RESET COUNT TO EVENT HANDLER OF MAIN MENU BUTTON
        resetter.KeyCountResetEvent += ResetFromResetter;

        //SUBSCRIBES RESET COUNT TO EVENT HANDLER OF VICTORY
        collectiblesUI.VictoryEvent += ResetAtVictory;

        //Restores found counter from memory
        KeySerializable keySerializable = new KeySerializable();
        keyCount = keySerializable.DeserializeInt();
    }

    // Update is called once per frame
    void Update()
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
}
