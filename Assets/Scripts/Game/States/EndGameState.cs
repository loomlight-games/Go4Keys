using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;
        Time.timeScale = 0f; // Stops simulation
        game.pauseMenu.EndResult();
    }

    public override void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game.replay = true;
        }

        Exit();
    }

    public override void Exit()
    {
        // Reload game if replay button is pressed
        if (game.replay)
        {
            Time.timeScale = 1f; // Resumes simulation
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
