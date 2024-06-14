using System;
using UnityEngine;

public class GamePlayState : AGameState
{
    GameObject buttons;
    GameObject pauseButton;
    string buttonClickedName = "None";
    string result;
    bool gameEnded = false;
    bool eventsSubscribed = false;

    public override void Enter()
    {
        buttons = GameObject.Find("Buttons");
        pauseButton = buttons.transform.Find("Pause").gameObject;
        pauseButton.SetActive(true);


        game.playerStaminaUI.SubscribeToStaminaChangeEvent();
        game.playerCollectedUI.Initialize();
        //game.autosave.Start();
        //game.gameButtonsUI.ShowPlayButtons();
        game.gameResultUI.HideAll();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            Player.Instance.endState.EndGameEvent += GameEnded;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            buttonClickedName = "Pause";

        //game.autosave.Update();
    }

    public override void Exit()
    {
        if (buttonClickedName == "Pause") // Pause button clicked or 'Esc' pressed
        {
            buttonClickedName = "None";
            pauseButton.SetActive(false);
            game.SetState(game.pauseState);
        }
        else if (gameEnded)
        {
            game.SetState(game.endState,result);
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        this.buttonClickedName = buttonName;
    }

    void GameEnded(object sender, string result)
    {
        gameEnded = true;
        this.result = result;
    }
}