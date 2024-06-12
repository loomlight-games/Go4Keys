
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameManager : AStateController
{
    #region CHARACTERS
    [Header("Characters")]
    public GameObject player;
    public GameObject enemy;
    #endregion

    #region STATES
    AGameState currentState;
    GamePlayState playState;
    #endregion

    #region EVENTS

    #endregion

    #region UI ELEMENTS
    [Header("----------------------- UI -----------------------")]
    [Header("Collectibles")]
    [Tooltip("It's the parent of the 'left' icons")]
    public GameObject leftUI;
    [Tooltip("It's the parent of the 'found' icons")]
    public GameObject foundUI;
    [Header("Stamina")]
    public Slider staminaBar;
    [Header("Pause menu")]
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject quitButton;
    [Header("Game results")]
    public GameObject victoryAdvice;
    public GameObject caughtAdvice;
    public GameObject staminaAdvice;
    [Header("Tutorial")]
    [Tooltip("It's the parent of the tutorial pop ups")]
    public GameObject popUps;
    #endregion

    public override void Start()
    {

    }

    public override void Update()
    {
        currentState.FrameUpdate();
    }

    public override IState GetState()
    {
        return currentState;
    }

    public override void SetState(IState state)
    {
        currentState = (AGameState) state;
        currentState.Enter(this);
    }
}
