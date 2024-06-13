using UnityEngine;

public class PauseGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;
        Time.timeScale = 0f;
        game.pauseMenu.Pause();
    }

    public override void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game.paused = false;
        }

        Exit();
    }

    public override void Exit()
    {
        // Switch to InGame if resume button is clicked
        if (!game.paused)
        {
            Time.timeScale = 1f;
            game.SetState(game.inGame);
        }
            
    }
}