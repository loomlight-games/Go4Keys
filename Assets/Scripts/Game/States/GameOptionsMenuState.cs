using UnityEngine;

/// <summary>
/// Handles music and effects volume, as well as tutorial visibility. Can switch to main menu state.
/// </summary>
public class GameOptionsMenuState : AGameState
{
    GameObject UI;
    readonly TutorialToggler tutorialToggler = new();
    string buttonClickedName = "None";
    bool alreadyEntered = false;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Options menu UI").gameObject;
        UI.SetActive(true);

        tutorialToggler.Initialize(); // Finds buttons and activate them according to saved data

        if (!alreadyEntered) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;

            alreadyEntered = true;
        }
    }

    public override void Exit()
    {
        switch (buttonClickedName)
        {
            case "Tutorial on":
                tutorialToggler.Activate(false);
                break;
            case "Tutorial off":
                tutorialToggler.Activate(true);
                break;
            case "Return":
                UI.SetActive(false);
                game.SetState(game.mainMenuState);
                break;
            default:
                break;
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        this.buttonClickedName = buttonName;
    }
}