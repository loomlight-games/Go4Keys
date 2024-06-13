using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Player player;
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
        player = Player.Instance;

        player.resilient.StaminaChangeEvent += StaminaRecover;
        player.jumper.JumpEvent += Jump;
        player.chaserResetter.ChaserResettedEvent += ChaserResetted;
        player.keyCollecter.CollectibleFoundEvent += CollectibleFound;
        player.turner.TurnedEvent += Turn;
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

    void CollectibleFound(object sender, EventArgs any)
    {
        collectibleFound.Play();
    }

    void Turn(object sender, bool turned)
    {
        if (turned)
            turn.Play();
    }
}
