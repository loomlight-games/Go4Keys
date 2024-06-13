using System;
using UnityEngine;

/// <summary>
/// Takes a turn at an intersection while still moving forward endlessly and changing rail.
/// Can be caught or return to run state when surpassing the center of the intersection.
/// </summary>
public class AtIntersectionState : APlayerState
{
    bool hasSurpassedTurnPoint = false;
    bool isCaught = false;
    bool eventsSubscribed = false;

    public override void Enter(AStateController controller)
    {
        player = (Player)controller;

        if (!eventsSubscribed) // Subscribe just once
        {
            player.turner.TurnedEvent += TurnPointSurpassed;
            player.chaserResetter.CaughtEvent += Caught;

            eventsSubscribed = true;
        }
    }
    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly, 
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Change rails
        player.turner.Update(); // Can take a turn to another street

        Exit();
    }
    public override void OnTriggerEnter(Collider other)
    {
        player.resilient.OnTriggerEnter(other); // Can recover stamina
        player.collecter.OnTriggerEnter(other); // Can find a collectible
        player.turner.OnTriggerEnter(other); // Sets the center of intersection
        player.chaserResetter.OnTriggerEnter(other); // Can reset chaser position
    }

    public override void OnCollisionEnter(Collision collision)
    {
        player.chaserResetter.OnCollisionEnter(collision); // Can be caught
    }

    public override void Exit()
    {
        if (hasSurpassedTurnPoint)
        {
            hasSurpassedTurnPoint = false;
            player.SetState(player.runState);
        }
        else if (isCaught)
        {
            //isCaught = false; No need bc won't return
            player.SetState(player.caughtState);
            
        }
    }

    void TurnPointSurpassed(object sender, bool turned)
    {
        hasSurpassedTurnPoint = true;
    }

    void Caught(object sender, EventArgs any)
    {
        isCaught = true;
    }
}
