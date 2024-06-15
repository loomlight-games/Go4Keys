using System;
using UnityEngine;

/// <summary>
/// Runs forward endlessly, losing stamina and changing rail.
/// Can enter an intersection, reset chaser position or be caught.
/// </summary>
public class PlayerRunState : APlayerState
{
    bool atIntersection = false;
    string result;
    bool alreadyEntered = false;

    public override void Enter()
    {
        if (!alreadyEntered)
        {
            player.endlessRunner.AtIntersectionEvent += AtIntersection;
            player.keyCollecter.AllFoundEvent += Victory;
            player.chased.CaughtEvent += Caught;
            player.resilient.StaminaChangeEvent += Tired;

            player.endlessRunner.Initialize(); // Detects obstacle checker
            player.railed.Initialize(); // Detects rails parent
            player.jumper.Initialize(); // Detects obstacle checker

            alreadyEntered = true;
        }
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly,
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Changes rails
    }

    public override void OnTriggerEnter(Collider other) 
    {
        player.endlessRunner.OnTriggerEnter(other); // Can enter an intersection
        player.resilient.OnTriggerEnter(other); // Can recover stamina
        player.keyCollecter.OnTriggerEnter(other); // Can find a collectible
        player.turner.OnTriggerEnter(other); // Set the center of intersection
        player.chased.OnTriggerEnter(other); // Can reset chaser position
    }

    public override void OnCollisionEnter(Collision collision)
    {
        player.chased.OnCollisionEnter(collision); // Can be caught
    }

    public override void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
            player.SetState(player.jumpState);
        }
        else if (atIntersection)
        {
            atIntersection = false;
            player.SetState(player.atIntersection);
        }
        else if (result != null)
        {
            player.SetState(player.endState, result);
        }
    }

    void AtIntersection(object sender, EventArgs any)
    {
        atIntersection = true;
    }

    void Victory(object sender, EventArgs e)
    {
        result = "Victory";
    }

    void Caught(object sender, EventArgs any)
    {
        result = "Caught";
    }

    void Tired(object sender, float stamina)
    {
        if (stamina <= 0)
        {
            result = "Tired";
        }
    }
}