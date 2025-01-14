using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Implements context and transitions between game states.
/// </summary>
public class GameManager : AStateController
{
    public static GameManager Instance;
    public event EventHandler<string> ButtonClicked;

    public TextMeshProUGUI debugText;

    #region STATES
    readonly GameMainMenuState mainMenuState = new();
    readonly GameOptionsMenuState optionsMenuState = new();
    readonly GameCreditsState creditsState = new();
    readonly GamePlayState playState = new();
    readonly GamePauseState pauseState = new();
    readonly GameEndState endState = new();
    #endregion

    #region UI
    public TutorialToggler tutorialToggler = new();
    public PlayerCollectiblesUI playerCollectedUI = new();
    public PlayerStaminaUI playerStaminaUI = new();
    public TutorialUI tutorialUI = new();
    #endregion

    public override void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public override void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            SetState(mainMenuState);
        }
        else // Gameplay scene
        {
            Player.Instance.PlayerResult += GameEnded;

            playerCollectedUI.Initialize();
            playerStaminaUI.Initialize();
            // TODO tutorialUI.Initialize();

            SetState(playState);
        }
    }

    public void ClickButton(string buttonName)
    {
        ButtonClicked?.Invoke(this, buttonName);

        switch (buttonName)
        {
            case "Start":
                SceneManager.LoadScene("Gameplay");
                break;
            case "Options":
                SwitchState(optionsMenuState);
                break;
            case "Tutorial on":
                if (currentState == optionsMenuState)
                    tutorialToggler.Activate(false);
                break;
            case "Tutorial off":
                if (currentState == optionsMenuState)
                    tutorialToggler.Activate(true);
                break;
            case "Credits":
                SwitchState(creditsState);
                break;
            case "Return":
                SwitchState(mainMenuState);
                break;
            case "Pause":
                SwitchState(pauseState);
                break;
            case "Resume":
                SwitchState(playState);
                break;
            case "Replay":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Main menu":
                SceneManager.LoadScene("Main Menu");
                break;
            case "Quit":
                Debug.Log("Quit");
                Application.Quit();
                break;
            default:
                break;
        }
    }

    void GameEnded(object sender, string result)
    {
        SwitchState(endState, result);
    }
}
