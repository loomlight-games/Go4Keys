using System;
using UnityEngine;

/// <summary>
/// Runs forward endlessly, losing stamina and changing rail.
/// Can enter an intersection, reset chaser position or be caught.
/// </summary>
public class PlayerRunState : APlayerState
{
    bool atIntersection = false;
    bool isCaught = false;
    bool eventsSubscribed = false;

    public override void Enter()
    {
        if (!eventsSubscribed) // Subscribe just once
        {
            player.endlessRunner.AtIntersectionEvent += AtIntersection;
            player.chaserResetter.CaughtEvent += Caught;

            eventsSubscribed = true;
        }
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly,
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Changes rails
        
        Exit();
    }

    public override void OnTriggerEnter(Collider other) 
    {
        player.endlessRunner.OnTriggerEnter(other); // Can enter an intersection
        player.resilient.OnTriggerEnter(other); // Can recover stamina
        player.keyCollecter.OnTriggerEnter(other); // Can find a collectible
        player.turner.OnTriggerEnter(other); // Set the center of intersection
        player.chaserResetter.OnTriggerEnter(other); // Can reset chaser position
    }

    public override void OnCollisionEnter(Collision collision)
    {
        player.chaserResetter.OnCollisionEnter(collision); // Can be caught
    }

    public override void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(player.jumpState);
        }
        else if (atIntersection)
        {
            atIntersection = false;
            player.SetState(player.atIntersection);
        }
        else if (isCaught)
        {
            //isCaught = false; No need bc won't return
            player.SetState(player.caughtState);
        }
    }

    void AtIntersection(object sender, EventArgs any)
    {
        atIntersection = true;
    }

    void Caught(object sender, EventArgs any)
    {
        isCaught = true;
    }
}