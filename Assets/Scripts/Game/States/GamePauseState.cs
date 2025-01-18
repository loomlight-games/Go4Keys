using UnityEngine;

/// <summary>
/// Stops simulation and can quit the game or switch to gameplay or main menu states.
/// </summary>
public class GamePauseState : AState
{
    GameObject buttons;
    GameObject resumeButton;
    GameObject mainMenuButton;
    GameObject quitButton;

    public override void Enter()
    {
        buttons = GameObject.Find("Buttons");
        resumeButton = buttons.transform.Find("Resume").gameObject;
        mainMenuButton = buttons.transform.Find("Main menu").gameObject;
        quitButton = buttons.transform.Find("Quit").gameObject;

        resumeButton.SetActive(true);
        mainMenuButton.SetActive(true);
        quitButton.SetActive(true);

        Time.timeScale = 0f; // Stops simulation


    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);

        // Restore all gamenager coroutines
    }
}