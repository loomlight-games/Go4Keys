using UnityEngine;

/// <summary>
/// Handles gameplay with the simulation going.
/// </summary>
public class GamePlayState : AState
{
    GameObject UI;

    public override void Enter()
    {
        Time.timeScale = 1f; // Resumes simulation

        UI = GameObject.Find("Buttons");
        UI = UI.transform.Find("Pause").gameObject;
        UI.SetActive(true);
    }

    public override void Update()
    {
        GameManager.Instance.tutorialUI.Update();
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }
}