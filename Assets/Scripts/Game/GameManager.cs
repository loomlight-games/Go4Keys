using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Implements context and transitions for game states.
/// </summary>
public class GameManager : AStateManager
{
    public static GameManager Instance;
    public event EventHandler<string> ButtonClicked;

    #region STATES
    public GameMainMenuState mainMenuState = new();
    public GameOptionsMenuState optionsMenuState = new();
    public GameCreditsState creditsState = new();
    public GamePlayState playState = new();
    public GamePauseState pauseState = new();
    public GameEndState endState = new();
    #endregion

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
            SetState(mainMenuState);
        else
            SetState(playState);
    }

    public void ClickButton(string buttonName)
    {
        ButtonClicked?.Invoke(this, buttonName);
    }
}
