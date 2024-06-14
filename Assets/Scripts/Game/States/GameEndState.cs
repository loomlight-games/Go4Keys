using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndState : AGameState
{
    GameObject buttons;
    GameObject replayButton;
    GameObject mainMenuButton;
    GameObject quitButton;
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

        Time.timeScale = 0f; // Stops simulation

        //game.gameButtonsUI.ShowEndButtons();

        if(result == "Victory")
            game.gameResultUI.ShowVictory();
        else if (result == "Caught")
            game.gameResultUI.ShowCaught();
        else if (result == "Tired") 
            game.gameResultUI.ShowTired();

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
        this.buttonClickedName = buttonName;
    }
}
