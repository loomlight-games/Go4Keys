using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main game menu and can quit game or switch to play, options or credits states.
/// </summary>
public class GameMainMenuState : AGameState
{
    GameObject UI;
    string buttonClickedName = "None";
    bool alreadyEntered = false;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Main menu UI").gameObject;
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
            case "Start":
                SceneManager.LoadScene("Level01");
                break;
            case "Options":
                UI.SetActive(false);
                game.SetState(game.optionsMenuState);
                break;
            case "Credits":
                UI.SetActive(false);
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