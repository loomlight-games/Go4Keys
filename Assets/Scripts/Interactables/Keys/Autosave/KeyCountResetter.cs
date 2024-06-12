using System;
using UnityEngine;

//HANDLES KEY COUNT RESET EVENT

public class KeyCountResetter : MonoBehaviour
{
     //EVENT HANDLER TO RESET KEY COUNT
    public event EventHandler KeyCountResetEvent;//Stores methods to invoke

    public void ResetKeyCount()
    {
        //Invokes methods in eventHandler
        KeyCountResetEvent?.Invoke(this, EventArgs.Empty);
    }
}
