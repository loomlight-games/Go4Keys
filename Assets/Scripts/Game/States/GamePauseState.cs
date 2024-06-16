using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stops simulation and can quit the game or switch to gameplay or main menu states.
/// </summary>
public class GamePauseState : AGameState
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
        if (Input.GetKeyDown(KeyCode.Escape))
            game.ClickButton("Resume");
    }

    public override void Exit()
    {
        Time.timeScale = 1f; // Resumes simulation

        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);
    }
}