using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stops simulation to show the result of the game. Can quit the game or switch to gameplay or main menu states.
/// </summary>
public class GameEndState : AGameState
{
    string buttonClickedName = "None";
    bool alreadyEntered = false;

    public override void Enter(string result)
    {
        Time.timeScale = 0f; // Stops simulation

        // Buttons
        GameObject buttons = GameObject.Find("Buttons");
        GameObject replayButton = buttons.transform.Find("Replay").gameObject;
        GameObject mainMenuButton = buttons.transform.Find("Main menu").gameObject;
        GameObject quitButton = buttons.transform.Find("Quit").gameObject;
        
        replayButton.SetActive(true);
        mainMenuButton.SetActive(true);
        quitButton.SetActive(true);

        // Tutorial pop ups
        GameObject tutorial = GameObject.Find("Tutorial pop ups");
        
        tutorial.SetActive(false);

        // Result pop ups
        GameObject results = GameObject.Find("Results");
        GameObject victoryResult = results.transform.Find("Victory").gameObject;
        GameObject caughtResult = results.transform.Find("Caught").gameObject;
        GameObject tiredResult = results.transform.Find("Tired").gameObject;

        if (result == "Victory")
            victoryResult.SetActive(true);
        else if (result == "Caught")
            caughtResult.SetActive(true);
        else if (result == "Tired")
            tiredResult.SetActive(true);

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
            buttonClickedName = "Replay";
            game.ClickButton("Replay");
        }
    }

    public override void Exit()
    {
        if (buttonClickedName == "Replay") // Replay button clicked or 'Esc' pressed
        {
            Time.timeScale = 1f; // Resumes simulation

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
