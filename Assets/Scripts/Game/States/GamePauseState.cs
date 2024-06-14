using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseState : AGameState
{
    bool resume = false;
    bool mainMenu = false;
    bool quit = false;
    bool eventsSubscribed = false;

    public override void Enter()
    {
        Time.timeScale = 0f; // Stops simulation

        game.gameButtonsUI.ShowPauseButtons();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.GameButtonClicked += ButtonClicked;
            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            resume = true;

        Exit();
    }

    public override void Exit()
    {
        if (resume) // Resume button clicked or 'Esc' pressed
        {
            Time.timeScale = 1f; // Resumes simulation

            game.SetState(game.playState);

            resume = false;
        }
        else if (mainMenu)
        {
            Debug.Log("To main menu");

            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene("Main Menu");
        }
        else if (quit)
        {
            Debug.Log("Quit game");

            Application.Quit();
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        if (buttonName == "Resume")
            resume = true;
        else if (buttonName == "MainMenu")
            mainMenu = true;
        else if (buttonName == "Quit")
            quit = true;
    }
}