using UnityEngine;

/// <summary>
/// Shows game credits. Can switch to main menu state
/// </summary>
public class GameCreditsState : AState
{
    GameObject UI;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Credits UI").gameObject;
        UI.SetActive(true);
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }
}