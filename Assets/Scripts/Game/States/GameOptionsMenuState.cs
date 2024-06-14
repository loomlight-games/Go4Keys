
using UnityEngine;

public class GameOptionsMenuState : AGameState
{
    GameObject UI;
    string buttonClickedName = "None";
    bool alreadyInitialized = false;
    readonly TutorialToggler tutorialToggler = new();

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Options menu UI").gameObject;
        UI.SetActive(true);

        tutorialToggler.Initialize(); // Finds buttons and activate them according to saved data

        if (!alreadyInitialized) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;

            alreadyInitialized = true;
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