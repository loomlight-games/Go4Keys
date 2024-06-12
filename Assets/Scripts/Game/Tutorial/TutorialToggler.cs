using System;
using UnityEngine;

//HANDLES TUTORIAL TOGGLING (DIRTY FLAG)

[Serializable]
public class TutorialToggler : MonoBehaviour
{
    //Dirty flag boolean
    bool dirtyTutorial = false;

    //Serializable object for tutorial visibility
    TutorialVisibility tutorialVisibility;

    //Visibility of tutorial
    [SerializeField] bool tutorialIsShown;//Can be changed in the editor

    //Buttons
    [SerializeField] GameObject activatedButton;
    [SerializeField] GameObject deactivatedButton;

    //Toggles (dirties) tutorial to activate it
    public void Activate()
    {
        dirtyTutorial = true;

        tutorialIsShown = true;

        //Toggles buttons
        activatedButton.SetActive(true);
        deactivatedButton.SetActive(false);
    }

    //Toggles (dirties) tutorial to deactivate it
    public void Deactivate()
    {
        dirtyTutorial = true;

        tutorialIsShown = false;

        //Toggles buttons
        activatedButton.SetActive(false);
        deactivatedButton.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantantiate serialized object
        tutorialVisibility = new TutorialVisibility();

        //Reads from memory if tutorial is activated
        tutorialIsShown = tutorialVisibility.Deserialize();

        //Activated at start
        if (tutorialIsShown)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Tutorial has been toggled
        if(dirtyTutorial)
        {
            //Creates serializable object
            tutorialVisibility = new TutorialVisibility(tutorialIsShown);

            //Saves in memory tutorial visibility
            tutorialVisibility.Serialize();

            //Cleans after saving
            dirtyTutorial = false;
        }
    }
}
