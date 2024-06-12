using System;
using UnityEngine;

//HANDLES UI COUNTER OF COLLECTIBLES

[Serializable]
public class CollectiblesUI : MonoBehaviour
{
    //EVENT HANDLER FOR VICTORY
    public event EventHandler VictoryEvent;//Stores methods to invoke when winning

    //SUBJECT
    [SerializeField] Collecter collecter;

    //DIRTYFLAG
    KeySerializable keySerializable;

    //UI elements
    [SerializeField] GameObject leftUI;//Contains all left collectibles icons in UI
    [SerializeField] GameObject foundUI;//Contains all found collectibles icons in UI
    
    //Arrays
    private GameObject[] leftArray;//Array of left collectibles in UI
    private GameObject[] foundArray;//Array of found collectibles in UI
    
    //Found objects counter
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        //Creates arrays
        leftArray = SetArray(leftUI);
        foundArray = SetArray(foundUI);

        ActivateArray(leftArray, true);
        ActivateArray(foundArray, false);

        //Restores found counter from memory
        keySerializable = new KeySerializable();
        counter = keySerializable.DeserializeInt();
        
        if (counter > -1)//At least one (0) collectible was found
        {
            RestoreUI(counter);
        }

        //UPDATEUI WHEN OBJECT COLLECTED
        collecter.ObjectCollectedEvent += UpdateUI;
    }

    //Fills array with children of a gameobject
    GameObject[] SetArray(GameObject GOgroup)
    {
        //Instantiate array of size of num of children
        GameObject[] array = new GameObject[GOgroup.transform.childCount];

        //Fills array
        for (int i = 0; i < GOgroup.transform.childCount; i++)
        {
            //With every gameobject in the group (child)
            array[i] = GOgroup.transform.GetChild(i).gameObject;
        }

        return array;
    }

    //Activate or deactivate elements of an array
    void ActivateArray(GameObject[] array, bool isActive)
    {
        //Fills array
        for (int i = 0; i < array.Length; i++)
        {
            array[i].SetActive(isActive);//Activates or deactivates each element
        }
    }

    //Updates UI, showing a found icon instead of a left one
    void UpdateUI(object sender, EventArgs e)
    {
        counter++;

        leftArray[counter].SetActive(false);
        foundArray[counter].SetActive(true);

        if (counter == foundArray.Length-1) {
            //Invokes methods when winning
            VictoryEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    //Restores found UI
    void RestoreUI(int index)
    {
        //Travel through arrays
        for (int i = 0; i <= index; i++) 
        {
            leftArray[i].SetActive(false);
            foundArray[i].SetActive(true);
        }
    }
}
