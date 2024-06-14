using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameState : AGameState
{
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
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            game.replay = true;

        Exit();
    }

    public override void Exit()
    {
        // Replay button clicked or 'Esc' pressed
        if (game.replay)
        {
            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (game.toMainMenu)
        {
            Debug.Log("To main menu");

            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene("Main Menu");
        }
        else if (game.quit)
        {
            Debug.Log("Quit game");

            Application.Quit();
        }
    }
}
