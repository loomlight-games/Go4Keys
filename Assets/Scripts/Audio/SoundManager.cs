using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    float lastStaminaValue = 100f;

    [SerializeField] AudioSource mainMenuMusic;
    [SerializeField] AudioSource gameplayMusic;
    [SerializeField] AudioSource staminaRecovered;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource chaserResetted;
    [SerializeField] AudioSource collectibleFound;
    [SerializeField] AudioSource turn;
    [SerializeField] AudioSource victory;
    [SerializeField] AudioSource defeat;
    [SerializeField] AudioSource pause;
    [SerializeField] AudioSource other;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ButtonClicked += ButtonClicked;

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            mainMenuMusic.Play();
        }
        else
        {
            gameplayMusic.Play();

            PlayerManager.Instance.resilient.StaminaChangeEvent += StaminaChange;
            PlayerManager.Instance.jumper.JumpEvent += Jump;
            PlayerManager.Instance.chased.ResetChaserEvent += ChaserResetted;
            PlayerManager.Instance.keyCollecter.KeyFoundEvent += CollectibleFound;
            PlayerManager.Instance.turner.TurnEvent += Turn;
            PlayerManager.Instance.keyCollecter.AllFoundEvent += Victory;
            PlayerManager.Instance.chased.CaughtEvent += Caught;
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        switch (buttonName)
        {
            case "Pause":
            case "Resume":
            case "Replay":
                pause.Play();
                break;
            default:
                other.Play();
                break;
        }
    }

    void StaminaChange(object sender, float stamina)
    {
        if (stamina < 0)
        {
            defeat.Play();
        }
        else if (stamina > lastStaminaValue)
        {
            staminaRecovered.Play();
        }

        lastStaminaValue = stamina;
    }

    void Jump(object sender, EventArgs any)
    {
        jump.Play();
    }

    void ChaserResetted(object sender, float distance)
    {
        chaserResetted.Play();
    }

    void CollectibleFound(object sender, int collected)
    {
        if (collected > 0)
            collectibleFound.Play();
    }

    void Turn(object sender, bool turned)
    {
        if (turned)
            turn.Play();
    }

    void Victory(object sender, EventArgs e)
    {
        victory.Play();
    }

    void Caught(object sender, EventArgs e)
    {
        defeat.Play();
    }
}
