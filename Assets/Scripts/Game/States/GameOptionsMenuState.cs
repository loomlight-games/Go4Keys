using UnityEngine;

/// <summary>
/// Handles music and effects volume, as well as tutorial visibility. Can switch to main menu state.
/// </summary>
public class GameOptionsMenuState : AState
{
    public readonly string TUTORIAL_FILE = "TutorialVisibility";
    GameObject UI, tutorialTogglers, teachedTutorialButton, notTeachedTutorialButton;
    bool alreadyEntered = false;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Options menu UI").gameObject;
        UI.SetActive(true);

        if (!alreadyEntered)
        {
            tutorialTogglers = GameObject.Find("Tutorial togglers");
            teachedTutorialButton = tutorialTogglers.transform.Find("Tutorial on").gameObject;
            notTeachedTutorialButton = tutorialTogglers.transform.Find("Tutorial off").gameObject;

            Recover();

            alreadyEntered = true;
        }
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }

    public void Recover()
    {
        // Retrieve the integer value from PlayerPrefs and convert it to a boolean
        int tutorialTeachedInt = PlayerPrefs.GetInt(TUTORIAL_FILE, 0);
        bool tutorialTeached = tutorialTeachedInt == 1;

        Debug.Log("Loaded tutorial teaching: " + tutorialTeached);
        ToggleButtonSprites(tutorialTeached);
    }

    public void TutorialTeaching(bool teached)
    {
        // Convert the boolean to an integer and store it in PlayerPrefs
        int teachedInt = teached ? 1 : 0;
        PlayerPrefs.SetInt(TUTORIAL_FILE, teachedInt);

        Debug.Log("Tutorial teaching: " + teached);
        ToggleButtonSprites(teached);
    }

    void ToggleButtonSprites(bool activated)
    {
        teachedTutorialButton.SetActive(activated);
        notTeachedTutorialButton.SetActive(!activated);
    }
}