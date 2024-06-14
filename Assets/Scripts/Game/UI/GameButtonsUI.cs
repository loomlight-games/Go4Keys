using UnityEngine;

/// <summary>
/// Handles gameplay buttons
/// </summary>
public class GameButtonsUI
{
    readonly GameObject pauseButton;
    readonly GameObject resumeButton;
    readonly GameObject replayButton;
    readonly GameObject mainMenuButton;
    readonly GameObject quitGameButton;

    public GameButtonsUI(GameObject pauseButton, GameObject resumeButton, GameObject replayButton, GameObject mainMenuButton, GameObject quitGameButton)
    {
        this.pauseButton = pauseButton;
        this.resumeButton = resumeButton;
        this.replayButton = replayButton;
        this.mainMenuButton = mainMenuButton;
        this.quitGameButton = quitGameButton;
    }

    public void ShowPlayButtons()
    {
        pauseButton.SetActive(true);
        mainMenuButton.SetActive(false);
        quitGameButton.SetActive(false);
        resumeButton.SetActive(false);
        replayButton.SetActive(false);
    }

    public void ShowPauseButtons()
    {
        mainMenuButton.SetActive(true);
        quitGameButton.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        replayButton.SetActive(false);
    }

    public void ShowEndButtons()
    {
        mainMenuButton.SetActive(true);
        quitGameButton.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        replayButton.SetActive(true);
    }
}
