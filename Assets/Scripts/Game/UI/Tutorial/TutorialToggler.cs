using UnityEngine;

/// <summary>
/// Toggles tutorial visibility and saves it in memory
/// </summary>
public class TutorialToggler
{
    readonly Serializable<bool> tutorialSerializable = new(); // Handles tutorial visibility
    readonly string tutorialFile = "tutorialVisibility.json";
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
        bool tutorialActivated = tutorialSerializable.Deserialize(tutorialFile);
        Toggle(tutorialActivated);
    }

    public void Activate(bool activated)
    {
        tutorialSerializable.Serialize(activated, tutorialFile);
        Toggle(activated);
    }

    void Toggle(bool activated)
    {
        activatedButton.SetActive(activated);
        deactivatedButton.SetActive(!activated);
    }
}
