using UnityEngine;

/// <summary>
/// Handles gameplay with the simulation going.
/// </summary>
public class GamePlayState : AState
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
            GameManager.Instance.playerCollectedUI.Initialize();
            GameManager.Instance.playerStaminaUI.Initialize();
            GameManager.Instance.tutorialUI.Initialize();

            alreadyEntered = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.ClickButton("Pause");

        GameManager.Instance.tutorialUI.Update();
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }
}