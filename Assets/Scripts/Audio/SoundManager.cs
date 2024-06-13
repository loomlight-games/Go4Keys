using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Player player;

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

        player.resilient.StaminaRecoverEvent += StaminaRecover;
        player.jumper.JumpEvent += Jump;
        player.chaserResetter.ChaserResettedEvent += ChaserResetted;
        player.collecter.CollectibleFoundEvent += CollectibleFound;
        player.turner.TurnedEvent += Turn;
    }

    void StaminaRecover(object sender, EventArgs any)
    {
        staminaRecovered.Play();
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
