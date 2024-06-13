using System;
using UnityEngine;

/// <summary>
/// Updates collected icons as the player collectes keys
/// </summary>
[Serializable]
public class CollectiblesUI
{
    public event EventHandler AllFoundEvent;

    readonly GameObject leftIcons;
    readonly GameObject foundIcons;
    private GameObject[] leftArray;
    private GameObject[] foundArray;
    private int collectedCounter = -1;
    //KeySerializable keySerializable;

    public CollectiblesUI(GameObject leftIcons, GameObject foundIcons)
    {
        this.leftIcons = leftIcons;
        this.foundIcons = foundIcons;
    }

    public void Start()
    {
        Player.Instance.keyCollecter.CollectibleFoundEvent += AddFoundIcon;

        //Creates arrays
        leftArray = SetArray(leftIcons);
        foundArray = SetArray(foundIcons);

        ActivateArray(leftArray, true);
        ActivateArray(foundArray, false);

        /*
        //Restores found counter from memory
        keySerializable = new KeySerializable();
        collectedCounter = keySerializable.DeserializeInt();
        */
        Debug.Log(collectedCounter);

        if (collectedCounter > -1)//At least one (0) collectible was found
            RestoreUI(collectedCounter);
        
    }

    /// <summary>
    /// Fills array with children of a gameobject
    /// </summary>
    GameObject[] SetArray(GameObject GOgroup)
    {
        //Instantiate array of size of num of children
        GameObject[] array = new GameObject[GOgroup.transform.childCount];

        //Fills array with every gameobject in the group (child)
        for (int i = 0; i < GOgroup.transform.childCount; i++)
            array[i] = GOgroup.transform.GetChild(i).gameObject;

        return array;
    }

    /// <summary>
    /// Activate or deactivate all elements of an array
    /// </summary>
    void ActivateArray(GameObject[] array, bool isActive)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i].SetActive(isActive);
        }
    }

    /// <summary>
    /// Updates UI, showing a found icon instead of a left one
    /// </summary>
    void AddFoundIcon(object sender, EventArgs e)
    {
        collectedCounter++;

        Debug.Log(collectedCounter);

        if (collectedCounter >= foundArray.Length - 1)
        {
            Debug.Log("All found");
            AllFoundEvent?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            leftArray[collectedCounter].SetActive(false);
            foundArray[collectedCounter].SetActive(true);
        }
    }

    /// <summary>
    /// Restores found UI
    /// </summary>
    void RestoreUI(int index)
    {
        for (int i = 0; i <= index; i++) 
        {
            leftArray[i].SetActive(false);
            foundArray[i].SetActive(true);
        }
    }
}
