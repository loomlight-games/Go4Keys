using System;
using UnityEngine;

/// <summary>
/// Updates collected icons as the player collectes keys
/// </summary>
[Serializable]
public class PlayerCollectiblesUI
{
    readonly GameObject leftIcons;
    readonly GameObject foundIcons;
    private GameObject[] leftArray;
    private GameObject[] foundArray;

    public PlayerCollectiblesUI(GameObject leftIcons, GameObject foundIcons)
    {
        this.leftIcons = leftIcons;
        this.foundIcons = foundIcons;
    }

    public void Initialize()
    {
        leftArray = SetArray(leftIcons);
        foundArray = SetArray(foundIcons);

        //ActivateArray(leftArray, true);
        //ActivateArray(foundArray, false);

        Player.Instance.keyCollecter.CollectibleFoundEvent += UpdateIcons;
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

    /*
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
    */

    /// <summary>
    /// Updates icons changing the left ones as the found ones
    /// </summary>
    void UpdateIcons(object sender, int collectedCount)
    {
        for (int i = 0; i < collectedCount; i++)
        {
            leftArray[i].SetActive(false);
            foundArray[i].SetActive(true);
        }
    }
}
