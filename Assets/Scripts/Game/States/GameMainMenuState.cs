using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainMenuState : AGameState
{
    string buttonClickedName = "None";
    bool eventsSubscribed = false;

    public override void Enter()
    {
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
            case "Start":
                SceneManager.LoadScene("Level01");
                break;
            case "Options":
                game.SetState(game.optionsMenuState);
                break;
            case "Credits":
                game.SetState(game.creditsState);
                break;
            case "Quit":
                Debug.Log("Quit");
                Application.Quit();
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