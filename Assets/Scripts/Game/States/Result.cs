using System;
using UnityEngine;

//HANDLES RESULT SHOW

public class Result : MonoBehaviour
{
    //EVENT HANDLER FOR END GAME
    public event EventHandler EndGameEvent;//Stores methods to invoke when game ends

    //SUBJECTS
    [SerializeField] GamePlayState collectibleUI;
    [SerializeField] StaminaSystem stamina;
    [SerializeField] Chaser chaser;

    //Sprites
    [SerializeField] GameObject victoryAdvice;
    [SerializeField] GameObject caughtAdvice;
    [SerializeField] GameObject staminaAdvice;

    //Sounds
    [SerializeField] AudioSource victorySound;
    [SerializeField] AudioSource lossSound;

    // Start is called before the first frame update
    void Start()
    {
        //SUBSCRIBES VICTORY TO EVENT HANDLER OF COLLECTIBLES UI
        collectibleUI.VictoryEvent += Victory;
        chaser.CaughtEvent += Caught;
        stamina.TiredEvent += Stamina;

        victoryAdvice.SetActive(false);
        caughtAdvice.SetActive(false);
        staminaAdvice.SetActive(false);
    }

    void Victory(object sender, EventArgs e)
    {   
        //Invokes methods in eventHandler
        EndGameEvent?.Invoke(this, EventArgs.Empty);

        victorySound.Play();

        victoryAdvice.SetActive(true);
    }
    void Caught(object sender, EventArgs e)
    {
        //Invokes methods in eventHandler
        EndGameEvent?.Invoke(this, EventArgs.Empty);

        lossSound.Play();

        caughtAdvice.SetActive(true);
    }

    void Stamina(object sender, EventArgs e)
    {
        //Invokes methods in eventHandler
        EndGameEvent?.Invoke(this, EventArgs.Empty);

        lossSound.Play();

        staminaAdvice.SetActive(true);
    }
}
