using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : AGameState
{
    GameObject buttons;
    GameObject replayButton;
    GameObject mainMenuButton;
    GameObject quitButton;
    GameObject results;
    GameObject victoryResult;
    GameObject caughtResult;
    GameObject tiredResult;
    string buttonClickedName = "None";
    bool eventsSubscribed = false;

    public override void Enter(string result)
    {
        buttons = GameObject.Find("Buttons");
        replayButton = buttons.transform.Find("Replay").gameObject;
        mainMenuButton = buttons.transform.Find("Main menu").gameObject;
        quitButton = buttons.transform.Find("Quit").gameObject;
        replayButton.SetActive(true);
        mainMenuButton.SetActive(true);
        quitButton.SetActive(true);

        results = GameObject.Find("Results");
        victoryResult = results.transform.Find("Victory").gameObject;
        caughtResult = results.transform.Find("Caught").gameObject;
        tiredResult = results.transform.Find("Tired").gameObject;

        Time.timeScale = 0f; // Stops simulation

        if(result == "Victory")
            victoryResult.SetActive(true);
        else if (result == "Caught")
            caughtResult.SetActive(true);
        else if (result == "Tired")
            tiredResult.SetActive(true);

        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            buttonClickedName = "Replay";
    }

    public override void Exit()
    {
        if (buttonClickedName == "Replay") // Replay button clicked or 'Esc' pressed
        {
            buttonClickedName = "None";
            replayButton.SetActive(false);
            mainMenuButton.SetActive(false);
            quitButton.SetActive(false);

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
