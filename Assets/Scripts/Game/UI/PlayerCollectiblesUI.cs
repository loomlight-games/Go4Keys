using System;
using UnityEngine;

/// <summary>
/// Updates collected icons as the player collectes keys
/// </summary>
[Serializable]
public class PlayerCollectiblesUI
{
    GameObject[] leftArray;
    GameObject[] foundArray;

    public void Initialize()
    {
        leftArray = SetArray(GameObject.Find("Remaining"));
        foundArray = SetArray(GameObject.Find("Found"));

        PlayerManager.Instance.keyCollecter.KeyFoundEvent += UpdateIcons;
    }

    /// <summary>
    /// Fills array with children of a gameobject
    /// </summary>
    GameObject[] SetArray(GameObject parent)
    {
        //Instantiate array of size of num of children
        GameObject[] array = new GameObject[parent.transform.childCount];

        //Fills array with every gameobject in the group (child)
        for (int i = 0; i < parent.transform.childCount; i++)
            array[i] = parent.transform.GetChild(i).gameObject;

        return array;
    }

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
