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
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game.replayGame = true;
        }

        Exit();
    }

    public override void Exit()
    {
        // Reload game if replay button is pressed
        if (game.replayGame)
        {
            Time.timeScale = 1f; // Resumes simulation
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
