using UnityEngine;
using UnityEngine.UIElements;

public class InGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        //game.GameButtonClicked += PauseButtonClicked;

        game.stamina.Start();
        game.collectibles.Start();
        //game.autosave.Start();
        game.pauseMenu.Resume();
    }

    public override void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game.paused = true;
        }

        //game.autosave.Update();

        Exit();
    }

    public override void Exit()
    {
        // Switch to Pause Menu if pause button is clicked
        if (game.paused)
            game.SetState(game.pausedGame);
        // Switch to endGame if any of the tree events are triggered
        else if (game.replay)
            game.SetState(game.endGame);
    }

    /*
    public void PauseButtonClicked(object sender, bool value)
    {
        paused = value;
    }
    */
}