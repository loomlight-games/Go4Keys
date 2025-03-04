using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
    public PlayerCollectiblesUI playerCollectedUI = new();
    public PlayerStaminaUI playerStaminaUI = new();
    public List<GameObject> tutorialPopUpsList = new();
    #endregion

    public const float SLOWED_SPEED = 0.5f, POPUP_DURATION = 2f, LONG_POPUP_DURATION = 4f;
    public float lastSimSpeed = 1f; // Simulation speed
    public const string TUTORIAL_FILE = "TutorialVisibility";

    public override void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
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
                    optionsMenuState.TutorialTeaching(false);
                break;
            case "Tutorial off":
                if (currentState == optionsMenuState)
                    optionsMenuState.TutorialTeaching(true);
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

    public void TutorialSequence()
    {
        StartCoroutine(TutorialSequenceCoroutine());
    }

    public void ActivateMomentarily(GameObject gameObject, float duration = POPUP_DURATION, float simSpeed = 1f)
    {
        StartCoroutine(ActivateMomentarilyCoroutine(gameObject, duration, simSpeed));
    }

    private IEnumerator TutorialSequenceCoroutine()
    {
        foreach (GameObject popUp in tutorialPopUpsList)
        {
            if (popUp.name == "Stamina" || popUp.name == "Keys" || popUp.name == "Telephone")
                yield return ActivateMomentarilyCoroutine(popUp, LONG_POPUP_DURATION, 0.1f);
            else
                yield return ActivateMomentarilyCoroutine(popUp, POPUP_DURATION);
        }
    }

    private IEnumerator ActivateMomentarilyCoroutine(GameObject gameObject, float duration, float simSpeed = 1f)
    {
        Time.timeScale = simSpeed; // Alters simulation speed
        lastSimSpeed = Time.timeScale;
        gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (currentState != pauseState)
            {
                elapsedTime += Time.unscaledDeltaTime;
            }
            yield return null;
        }

        gameObject.SetActive(false);
        Time.timeScale = 1f; // Resets simulation speed
        lastSimSpeed = Time.timeScale;
    }
}
