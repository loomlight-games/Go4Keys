using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    float lastStaminaValue = 100f;

    // Game
    [SerializeField] AudioSource staminaRecovered;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource chaserResetted;
    [SerializeField] AudioSource collectibleFound;
    [SerializeField] AudioSource turn;
    // UI
    [SerializeField] AudioSource pause;
    [SerializeField] AudioSource victory;
    [SerializeField] AudioSource defeat;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.resilient.StaminaChangeEvent += StaminaRecover;
        Player.Instance.jumper.JumpEvent += Jump;
        Player.Instance.chased.ChaserResettedEvent += ChaserResetted;
        Player.Instance.keyCollecter.CollectibleFoundEvent += CollectibleFound;
        Player.Instance.turner.TurnedEvent += Turn;
    }

    void StaminaRecover(object sender, float stamina)
    {
        if (stamina > lastStaminaValue)
            staminaRecovered.Play();

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
        collectibleFound.Play();
    }

    void Turn(object sender, bool turned)
    {
        if (turned)
            turn.Play();
    }
}
