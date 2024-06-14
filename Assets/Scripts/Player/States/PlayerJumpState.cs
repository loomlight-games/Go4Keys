using System;
using UnityEngine;

/// <summary>
/// Jumps once while still moving forward endlessly and changing rail.
/// Can enter an intersection or be caught.
/// </summary>
public class PlayerJumpState : APlayerState
{
    bool hasSurpassedTurnPoint = true;
    bool isCaught = false;
    bool eventsSubscribed = false;

    public override void Enter()
    {
        player.jumper.Jump();// Jumps
        player.resilient.Jumps();// Jumped, thus, loses a lot of stamina

        if (!eventsSubscribed) // Subscribe just once
        {
            player.endlessRunner.AtIntersectionEvent += AtIntersection;
            player.turner.TurnedEvent += TurnPointSurpassed;
            player.chased.CaughtEvent += Caught;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Continues moving forward
        player.railed.Update(); // Change rails
    }

    public override void OnTriggerEnter(Collider other)
    {
        player.endlessRunner.OnTriggerEnter(other); // Can enter an intersection
        player.resilient.OnTriggerEnter(other); // Can recover stamina
        player.keyCollecter.OnTriggerEnter(other); // Can find a collectible
        player.chased.OnTriggerEnter(other); // Can reset chaser position
    }

    public override void Exit()
    {
        if (player.jumper.IsGrounded())
        {
            player.SetState(player.runState);
        }
        if (!hasSurpassedTurnPoint)
        {
            player.SetState(player.atIntersection);
        }
        else if (isCaught)
        {
            //isCaught = false; No need bc won't return
            player.SetState(player.endState);
        }
    }

    void AtIntersection(object sender, EventArgs any)
    {
        hasSurpassedTurnPoint = false;
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
