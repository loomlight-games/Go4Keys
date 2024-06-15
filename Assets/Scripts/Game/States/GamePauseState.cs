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
    string buttonClickedName = "None";
    bool alreadyEntered = false;

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

        if (!alreadyEntered) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            alreadyEntered = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonClickedName = "Resume";
            game.ClickButton("Resume");
        }
    }

    public override void Exit()
    {
        if (buttonClickedName == "Resume") // Resume button clicked or 'Esc' pressed
        {
            buttonClickedName = "None";
            resumeButton.SetActive(false);
            mainMenuButton.SetActive(false);
            quitButton.SetActive(false);

            Time.timeScale = 1f; // Resumes simulation

            game.SetState(game.playState);
        }
        else if (buttonClickedName == "Main menu")
        {
            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene("Main Menu");
        }
        else if (buttonClickedName == "Quit")
        {
            Debug.Log("Quit game");

            Application.Quit();
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        buttonClickedName = buttonName;
    }
}