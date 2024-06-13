using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameManager : AStateController
{
    public static GameManager Instance;
    [HideInInspector] public bool gamePaused = false;
    [HideInInspector] public bool replayGame = false;
    [HideInInspector] public bool playerVictory = false;
    [HideInInspector] public bool playerCaught = false;
    [HideInInspector] public bool playerTired = false;

    #region EVENTS
    //public event EventHandler<bool> GameButtonClicked;
    #endregion

    #region STATES
    // Main menu
    // Options 
    // Credits
    public InGameState inGame = new();
    public PauseGameState pausedGame = new();
    public EndGameState endGame = new();
    #endregion

    #region BEHAVIOURS
    public PlayerCollectedUI playerCollectedUI;
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

        SetState(inGame);
    }

    public void PauseGame(bool paused)
    {
        //GameButtonClicked?.Invoke(this, paused);
        //Debug.Log(paused);
        gamePaused = paused;
    }

    public void ReplayGame()
    {
        //ReplayButtonClicked?.Invoke(this, paused);
        //Debug.Log(gamePaused);
        replayGame = true;
    }
}
