using System;
using UnityEngine;

//HANDLES RESULT SHOW

public class Result : MonoBehaviour
{
    //EVENT HANDLER FOR END GAME
    public event EventHandler EndGameEvent;//Stores methods to invoke when game ends

    //SUBJECTS
    [SerializeField] CollectiblesUI collectibleUI;
    Player player;

    //Sprites
    [SerializeField] GameObject victoryAdvice;
    [SerializeField] GameObject caughtAdvice;
    [SerializeField] GameObject staminaAdvice;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;

        collectibleUI.VictoryEvent += Victory;
        player.chaserResetter.CaughtEvent += Caught;
        player.resilient.StaminaChangeEvent += Stamina;

        victoryAdvice.SetActive(false);
        caughtAdvice.SetActive(false);
        staminaAdvice.SetActive(false);
    }

    void Victory(object sender, EventArgs e)
    {   
        EndGameEvent?.Invoke(this, EventArgs.Empty);
        victoryAdvice.SetActive(true);
    }
    void Caught(object sender, EventArgs e)
    {
        EndGameEvent?.Invoke(this, EventArgs.Empty);
        caughtAdvice.SetActive(true);
    }

    void Stamina(object sender, float stamina)
    {
        if (stamina < 0)
        {
            EndGameEvent?.Invoke(this, EventArgs.Empty);
            staminaAdvice.SetActive(true);
        }
    }
}
