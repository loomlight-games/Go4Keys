using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameManager : AStateController
{
    public static GameManager Instance;
    public bool paused = false;
    public bool replay = false;

    #region EVENTS
    //public event EventHandler<bool> GameButtonClicked;
    #endregion

    #region STATES
    // Main menu
    // Options 
    // Credits
    public InGameState inGame = new();// InGame
    public PauseGameState pausedGame = new(); // Pause
    public EndGameState endGame = new(); // EndGame
    #endregion

    #region BEHAVIOURS
    public CollectiblesUI collectibles;
    public StaminaBar stamina;
    //public KeyAutosave autosave;
    public PauseMenu pauseMenu;
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
        collectibles = new(leftIcons, foundIcons);
        stamina = new(staminaBar);
        //autosave = new();
        pauseMenu = new(pauseButton, resumeButton, replayButton, mainMenuButton, quitGameButton);

        SetState(inGame);
    }

    public void PauseGame(bool paused)
    {
        //GameButtonClicked?.Invoke(this, paused);
        Debug.Log(paused);
        this.paused = paused;
    }

    public void ReplayGame()
    {
        //ReplayButtonClicked?.Invoke(this, paused);
        Debug.Log(paused);
        replay = true;
    }
}
