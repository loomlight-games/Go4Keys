using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        Time.timeScale = 0f; // Stops simulation

        game.gameButtonsUI.ShowPauseButtons();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            game.paused = false;

        Exit();
    }

    public override void Exit()
    {
        // Resume button clicked or 'Esc' pressed
        if (!game.paused)
        {
            Time.timeScale = 1f; // Resumes simulation

            game.SetState(game.inGame);
        }
        else if (game.toMainMenu)
        {
            Debug.Log("To main menu");

            Time.timeScale = 1f; // Resumes simulation

            //Loads main menu scene
            SceneManager.LoadScene("Main Menu");
        }
        else if (game.quit)
        {
            Debug.Log("Quit game");

            Application.Quit();
        }
    }
}