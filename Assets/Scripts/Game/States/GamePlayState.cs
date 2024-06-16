using UnityEngine;

/// <summary>
/// Handles gameplay with the simulation going.
/// </summary>
public class GamePlayState : AGameState
{
    GameObject UI;
    bool alreadyEntered = false;

    public override void Enter()
    {
        Time.timeScale = 1f; // Resumes simulation

        UI = GameObject.Find("Buttons");
        UI = UI.transform.Find("Pause").gameObject;
        UI.SetActive(true);

        if (!alreadyEntered)
        {
            game.playerCollectedUI.Initialize();
            game.playerStaminaUI.Initialize();
            game.tutorialUI.Initialize();

            alreadyEntered = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            game.ClickButton("Pause");

        game.tutorialUI.Update();
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }
}