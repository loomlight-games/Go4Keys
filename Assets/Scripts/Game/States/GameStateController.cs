
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Implements context methods 
/// </summary>
public class GameStateController : AStateController
{

    #region UI elements
    [Header("Fixers")]
    public GameObject leftUI, foundUI;
    #endregion

    public override void Start()
    {

    }

    public override void Update()
    {
        currentState.Update();
    }

    public override IState GetState()
    {
        return currentState;
    }

    public override void SetState(IState state)
    {
        currentState = (AGameState) state;
    }
}
