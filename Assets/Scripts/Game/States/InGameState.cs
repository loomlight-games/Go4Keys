using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameState : AGameState
{
    bool eventsSubscribed = false;

    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        //game.GameButtonClicked += PauseButtonClicked;

        game.playerStaminaUI.SubscribeToStaminaChangeEvent();
        game.playerCollectedUI.Initialize();
        //game.autosave.Start();
        game.gameButtonsUI.ShowPlayButtons();
        game.gameResultUI.HideAll();

        if (!eventsSubscribed) // Subscribe just once
        {
            game.playerCollectedUI.AllFoundEvent += Victory;
            Player.Instance.chaserResetter.CaughtEvent += Caught;
            Player.Instance.resilient.StaminaChangeEvent += Tired;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            game.gamePaused = true;

        //game.autosave.Update();

        Exit();
    }

    public override void Exit()
    {
        if (game.gamePaused) // Pause button clicked or 'Esc' pressed
            game.SetState(game.pausedGame);
        else if (game.playerVictory || game.playerCaught || game.playerTired)
            game.SetState(game.endGame);
    }

    /*
    public void PauseButtonClicked(object sender, bool value)
    {
        paused = value;
    }
    */

    void Victory(object sender, EventArgs e)
    {
        game.playerVictory = true;
    }

    void Caught(object sender, EventArgs e)
    {
        game.playerCaught = true;
    }

    void Tired(object sender, float stamina)
    {
        if (stamina <= 0)
        {
            game.playerTired = true;
        }
    }
}