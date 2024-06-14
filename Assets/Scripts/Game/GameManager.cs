using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameManager : AStateController
{
    public static GameManager Instance;
    public event EventHandler<string> GameButtonClicked;

    [HideInInspector] public bool playerVictory = false;
    [HideInInspector] public bool playerCaught = false;
    [HideInInspector] public bool playerTired = false;

    #region STATES
    // Main menu
    // Options 
    // Credits
    public GamePlayState playState = new();
    public GamePauseState pauseState = new();
    public GameEndState endState = new();
    #endregion

    #region BEHAVIOURS
    public PlayerCollectiblesUI playerCollectedUI;
    public PlayerStaminaUI playerStaminaUI;
    //public KeyAutosave autosave;
    public GameButtonsUI gameButtonsUI;
    public GameResultUI gameResultUI;
    #endregion

    #region GRAPHICS
    [Header("Collectibles")]
    [Tooltip("Contains all left collectibles icons in UI")]
    [SerializeField] GameObject leftIcons;
    [Tooltip("Contains all found collectibles icons in UI")]
    [SerializeField] GameObject foundIcons;

    [Header("Stamina bar")]
    [SerializeField] Slider staminaBar;

    [Header("Pause menu")]
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject quitGameButton;

    [Header("End game pop ups")]
    [SerializeField] GameObject victory;
    [SerializeField] GameObject caught;
    [SerializeField] GameObject tired;

    //TUTORIAL
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
        playerCollectedUI = new(leftIcons, foundIcons);
        playerStaminaUI = new(staminaBar);
        //autosave = new();
        gameButtonsUI = new(pauseButton, resumeButton, replayButton, mainMenuButton, quitGameButton);
        gameResultUI = new(victory, caught, tired);

        SetState(playState);
    }

    public void ClickGameButton(string buttonName)
    {
        GameButtonClicked?.Invoke(this, buttonName);
    }
}
