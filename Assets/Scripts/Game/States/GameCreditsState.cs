
using UnityEngine;

public class GameCreditsState : AGameState
{
    GameObject UI;

    string buttonClickedName = "None";
    bool eventsSubscribed = false;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Credits UI").gameObject;
        UI.SetActive(true);

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            eventsSubscribed = true;
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