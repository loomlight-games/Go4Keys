using UnityEngine;

public class GamePlayState : AGameState
{
    GameObject buttons;
    GameObject pauseButton;
    string buttonClickedName = "None";
    string result = "None";
    bool eventsSubscribed = false;
    PlayerCollectiblesUI playerCollectedUI;
    PlayerStaminaUI playerStaminaUI;
    TutorialManager tutorialManager;

    public override void Enter()
    {
        buttons = GameObject.Find("Buttons");
        pauseButton = buttons.transform.Find("Pause").gameObject;
        pauseButton.SetActive(true);
        //game.autosave.Start();

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            Player.Instance.endState.EndGameEvent += GameEnded;

            playerCollectedUI = new();
            playerStaminaUI = new();
            tutorialManager = new();
            playerCollectedUI.Initialize();
            playerStaminaUI.Initialize();
            tutorialManager.Initialize();

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            buttonClickedName = "Pause";

        //game.autosave.Update();
        tutorialManager.Update();
    }

    public override void Exit()
    {
        if (buttonClickedName == "Pause") // Pause button clicked or 'Esc' pressed
        {
            buttonClickedName = "None";
            pauseButton.SetActive(false);
            game.SetState(game.pauseState);
        }
        else if (result != "None") // Result has been received
        {
            game.SetState(game.endState,result);
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        buttonClickedName = buttonName;
    }

    void GameEnded(object sender, string result)
    {
        this.result = result;
    }
}