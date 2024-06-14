using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : AGameState
{
    bool replay = false;
    bool mainMenu = false;
    bool quit = false;
    bool eventsSubscribed = false;

    public override void Enter(string result)
    {
        Time.timeScale = 0f; // Stops simulation

        game.gameButtonsUI.ShowEndButtons();

        if(result == "Victory")
            game.gameResultUI.ShowVictory();
        else if (result == "Caught")
            game.gameResultUI.ShowCaught();
        else if (result == "Tired") 
            game.gameResultUI.ShowTired();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            replay = true;
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
