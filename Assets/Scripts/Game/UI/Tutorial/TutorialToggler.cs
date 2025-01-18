using UnityEngine;

/// <summary>
/// Toggles tutorial visibility and saves it in memory
/// </summary>
public class TutorialToggler
{
    //readonly Serializable<bool> tutorialSerializable = new(); // Handles tutorial visibility
    //readonly string tutorialFile = "tutorialVisibility.json";
    public readonly string TUTORIAL_FILE = "TutorialVisibility";
    GameObject togglers, activatedButton, deactivatedButton;

    public void Initialize()
    {
        togglers = GameObject.Find("Tutorial togglers");
        activatedButton = togglers.transform.Find("Tutorial on").gameObject;
        deactivatedButton = togglers.transform.Find("Tutorial off").gameObject;

        Recover();
    }

    public void Recover()
    {
        //bool tutorialActivated = tutorialSerializable.Deserialize(tutorialFile);

        // Retrieve the integer value from PlayerPrefs and convert it to a boolean
        int tutorialActivatedInt = PlayerPrefs.GetInt(TUTORIAL_FILE, 0);
        bool tutorialActivated = tutorialActivatedInt == 1;

        Debug.Log("Loaded tutorial visibility: " + tutorialActivated);
        ToggleButtonSprites(tutorialActivated);
    }

    public void Activate(bool activated)
    {
        //tutorialSerializable.Serialize(activated, tutorialFile);

        // Convert the boolean to an integer and store it in PlayerPrefs
        int activatedInt = activated ? 1 : 0;
        PlayerPrefs.SetInt(TUTORIAL_FILE, activatedInt);

        Debug.Log("Saved tutorial visibility: " + activated);
        ToggleButtonSprites(activated);
    }

    void ToggleButtonSprites(bool activated)
    {
        activatedButton.SetActive(activated);
        deactivatedButton.SetActive(!activated);
    }
}
