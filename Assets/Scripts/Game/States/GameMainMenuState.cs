using UnityEngine;

public class GameMainMenuState : AGameState
{
    GameObject UI;

    public override void Enter()
    {
        UI = GameObject.Find("UI");
        UI = UI.transform.Find("Main menu UI").gameObject;
        UI.SetActive(true);
    }

    public override void Exit()
    {
        UI.SetActive(false);
    }
}