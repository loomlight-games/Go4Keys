using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//HANDLES PAUSE MENU

public class PauseMenu
{
    //SUBJECT
    [SerializeField] Result result;

    //Buttons
    readonly GameObject pauseButton;
    readonly GameObject resumeButton;
    readonly GameObject replayButton;
    readonly GameObject mainMenuButton;
    readonly GameObject quitGameButton;

    //Game states
    private bool gameInPause = false;
    private bool gameEnded = false;

    public PauseMenu(GameObject pauseButton, GameObject resumeButton, GameObject replayButton, GameObject mainMenuButton, GameObject quitGameButton)
    {
        this.pauseButton = pauseButton;
        this.resumeButton = resumeButton;
        this.replayButton = replayButton;
        this.mainMenuButton = mainMenuButton;
        this.quitGameButton = quitGameButton;
    }

    public void Start()
    {
        //SUBSCRIBES ENDRESULT TO EVENT HANDLER OF RESULT
        result.EndGameEvent += EndResult;

        Resume();
    }

    public void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Game hasn't finished
            if (!gameEnded)
            {
                //Game paused
                if (gameInPause)
                {
                    Resume();
                }
                //Game unpaused
                else
                {
                    Pause();
                }
            }
            //Game has finished
            else
            {
                //Replays game
                ReplayGame();
            }
            
        }
    }

    /// <summary>
    /// End result UI (shows replay game button as well)
    /// </summary>
    public void EndResult(object sender, EventArgs e)
    {
        //Time.timeScale = 0f;

        //gameEnded = true;

        mainMenuButton.SetActive(true);
        quitGameButton.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        replayButton.SetActive(true);
    }

    public void EndResult()
    {
        //Time.timeScale = 0f;

        //gameEnded = true;

        mainMenuButton.SetActive(true);
        quitGameButton.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        replayButton.SetActive(true);
    }

    /// <summary>
    /// Resumes simulation
    /// </summary>
    public void Resume()
    {
        //Time.timeScale = 1f;

        //gameInPause = false;

        pauseButton.SetActive(true);
        mainMenuButton.SetActive(false);
        quitGameButton.SetActive(false);
        resumeButton.SetActive(false);
        replayButton.SetActive(false);
    }

    /// <summary>
    /// Stops simulation
    /// </summary>
    public void Pause()
    {
        //Time.timeScale = 0f;

        //gameInPause = true;

        mainMenuButton.SetActive(true);
        quitGameButton.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        replayButton.SetActive(false);
    }

    /// <summary>
    /// Replays game
    /// </summary>
    public void ReplayGame()
    {
        Time.timeScale = 1f;//Resumes simulation

        //Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        //Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Loads MainMenu scene
    /// </summary>
    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//Loads main menu scene
    }

    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

}
