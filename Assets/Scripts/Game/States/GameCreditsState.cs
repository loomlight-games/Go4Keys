using UnityEngine;

/// <summary>
/// Shows game credits. Can switch to main menu state
/// </summary>
public class GameCreditsState : AGameState
{
    GameObject UI;
    string buttonClickedName = "None";
    bool alreadyEntered = false;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Credits UI").gameObject;
        UI.SetActive(true);

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