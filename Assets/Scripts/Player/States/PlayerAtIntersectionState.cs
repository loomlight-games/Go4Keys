using System;
using UnityEngine;

/// <summary>
/// Takes a turn at an intersection while still moving forward endlessly and changing rail.
/// Can be caught or return to run state when surpassing the center of the intersection.
/// </summary>
public class PlayerAtIntersectionState : APlayerState
{
    bool hasSurpassedTurnPoint = false;
    string result;
    bool alreadyEntered = false;

    public override void Enter()
    {
        if (!alreadyEntered)
        {
            player.turner.TurnedEvent += TurnPointSurpassed;
            player.keyCollecter.AllFoundEvent += Victory;
            player.chased.CaughtEvent += Caught;
            player.resilient.StaminaChangeEvent += Tired;

            alreadyEntered = true;
        }
    }
    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly, 
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Change rails
        player.turner.Update(); // Can take a turn to another street
    }
    public override void OnTriggerEnter(Collider other)
    {
        player.resilient.OnTriggerEnter(other); // Can recover stamina
        player.keyCollecter.OnTriggerEnter(other); // Can find a collectible
        player.turner.OnTriggerEnter(other); // Sets the center of intersection
        player.chased.OnTriggerEnter(other); // Can reset chaser position
    }

    public override void OnCollisionEnter(Collision collision)
    {
        player.chased.OnCollisionEnter(collision); // Can be caught
    }

    public override void Exit()
    {
        if (hasSurpassedTurnPoint)
        {
            hasSurpassedTurnPoint = false;
            player.SetState(player.runState);
        }
        else if (result != null)
        {
            player.SetState(player.endState, result);
        }
    }

    void TurnPointSurpassed(object sender, bool turned)
    {
        hasSurpassedTurnPoint = true;
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
