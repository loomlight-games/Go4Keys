using System;
using UnityEngine;

public class GamePlayState : AGameState
{
    bool pause = false;
    bool gameEnded = false;
    string result;
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
            Player.Instance.endState.EndGameEvent += GameEnded;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause = true;

        //game.autosave.Update();
    }

    public override void Exit()
    {
        if (pause) // Pause button clicked or 'Esc' pressed
        {
            pause = false;
            game.SetState(game.pauseState);
        }
        else if (gameEnded)
        {
            game.SetState(game.endState,result);
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        if (buttonName == "Pause")
            pause = true;
    }

    void GameEnded(object sender, string result)
    {
        gameEnded = true;
        this.result = result;
    }
}