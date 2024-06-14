using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : AGameState
{
    bool replay = false;
    bool mainMenu = false;
    bool quit = false;
    bool eventsSubscribed = false;

    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        Time.timeScale = 0f; // Stops simulation

        game.gameButtonsUI.ShowEndButtons();

        if(game.playerVictory)
            game.gameResultUI.ShowVictory();
        else if (game.playerCaught)
            game.gameResultUI.ShowCaught();
        else if (game.playerTired) 
            game.gameResultUI.ShowTired();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.GameButtonClicked += ButtonClicked;
            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            replay = true;

        Exit();
    }

    public override void Exit()
    {
        if (replay) // Replay button clicked or 'Esc' pressed
        {
            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (buttonName == "Replay")
            replay = true;
        else if (buttonName == "MainMenu")
            mainMenu = true;
        else if (buttonName == "Quit")
            quit = true;
    }
}
