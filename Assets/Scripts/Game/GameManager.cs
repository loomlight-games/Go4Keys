using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameManager : AStateController
{
    public static GameManager Instance;

    #region STATES
    // Main menu
    // Options 
    // Credits
    //public InGameState inGame = new();// InGame
    // Pause
    // EndGame
    #endregion

    #region BEHAVIOURS
    //public CollectiblesUI collectibles;
    //public StaminaBarUI stamina;
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
        //collectibles = new(leftIcons, foundIcons);
        //stamina = new(staminaBar);

        //SetState(inGame);
    }
}
