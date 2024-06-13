using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//HANDLES PAUSE MENU

public class PauseMenu : MonoBehaviour
{
    //SUBJECT
    [SerializeField] Result result;

    //Buttons
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject quitButton;

    //Game states
    private bool gameInPause = false;
    private bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        //SUBSCRIBES ENDRESULT TO EVENT HANDLER OF RESULT
        result.EndGameEvent += EndResult;

        Resume();
    }

    // Update is called once per frame
    void Update()
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

    //End result UI (shows replay game button as well)
    void EndResult(object sender, EventArgs e)
    {
        Time.timeScale = 0f;

        gameEnded = true;

        menuButton.SetActive(true);
        quitButton.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        replayButton.SetActive(true);
    }

    //Resumes simulation
    public void Resume()
    {
        Time.timeScale = 1f;

        gameInPause = false;

        pauseButton.SetActive(true);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        resumeButton.SetActive(false);
        replayButton.SetActive(false);

    }

    //Stops simulation
    public void Pause()
    {
        Time.timeScale = 0f;

        gameInPause = true;

        menuButton.SetActive(true);
        quitButton.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        replayButton.SetActive(false);
    }

    //Replays game
    public void ReplayGame()
    {
        Time.timeScale = 1f;//Resumes simulation

        //Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        //Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//Loads main menu scene
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
