using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stops simulation to show the result of the game. Can quit the game or switch to gameplay or main menu states.
/// </summary>
public class GameEndState : AState
{
    GameObject replayButton, mainMenuButton, quitButton, resultPopUp;

    public override void Enter(string result)
    {
        Time.timeScale = 0f; // Stops simulation

        // Buttons
        GameObject buttons = GameObject.Find("Buttons");
        replayButton = buttons.transform.Find("Replay").gameObject;
        mainMenuButton = buttons.transform.Find("Main menu").gameObject;
        quitButton = buttons.transform.Find("Quit").gameObject;

        replayButton.SetActive(true);
        mainMenuButton.SetActive(true);
        quitButton.SetActive(true);

        // Tutorial pop ups
        GameObject tutorial = GameObject.Find("Tutorial pop ups");

        tutorial?.SetActive(false);

        // Result pop ups
        GameObject results = GameObject.Find("Results");
        GameObject victoryResult = results.transform.Find("Victory").gameObject;
        GameObject caughtResult = results.transform.Find("Caught").gameObject;
        GameObject tiredResult = results.transform.Find("Tired").gameObject;

        if (result == "Victory")
            resultPopUp = victoryResult;
        else if (result == "Caught")
            resultPopUp = caughtResult;
        else if (result == "Tired")
            resultPopUp = tiredResult;

        resultPopUp?.SetActive(true);

        // Deactivate touch input
        InputManager.Instance.gameObject.SetActive(false);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        Time.timeScale = 1f; // Resumes simulation

        replayButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);
        resultPopUp.SetActive(false);
    }
}
