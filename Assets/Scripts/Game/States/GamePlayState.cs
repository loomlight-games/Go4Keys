using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayState : AGameState
{
    bool pause = false;
    bool eventsSubscribed = false;

    public override void Enter()
    {
        game.playerStaminaUI.SubscribeToStaminaChangeEvent();
        game.playerCollectedUI.Initialize();
        //game.autosave.Start();
        game.gameButtonsUI.ShowPlayButtons();
        game.gameResultUI.HideAll();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.GameButtonClicked += ButtonClicked;

            game.playerCollectedUI.AllFoundEvent += Victory;
            Player.Instance.chaserResetter.CaughtEvent += Caught;
            Player.Instance.resilient.StaminaChangeEvent += Tired;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause = true;

        //game.autosave.Update();

        Exit();
    }

    public override void Exit()
    {
        if (pause) // Pause button clicked or 'Esc' pressed
        {
            pause = false;
            game.SetState(game.pauseState);
        }
        else if (game.playerVictory || game.playerCaught || game.playerTired)
        {
            game.SetState(game.endState);
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        if (buttonName == "Pause")
            pause = true;
    }

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