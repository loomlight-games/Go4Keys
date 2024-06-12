using UnityEngine;

/// <summary>
/// Takes a turn at an intersection while still moving forward endlessly and changing rail
/// </summary>
public class AtIntersectionState : APlayerState
{
    public override void Enter(AStateController controller)
    {
        player = (Player)controller;
    }
    public override void Update()
    {
        
        player.endlessRunner.Update(); // Runs endlessly, 
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Change rails
        player.turner.Update(); // Can take a turn to another street

        // Checks exit
        Exit();
    }
    public override void OnTriggerEnter(Collider other)
    {
        player.resilient.OnTriggerEnter(other);
        player.collecter.OnTriggerEnter(other);
        player.turner.OnTriggerEnter(other);
    }

    public override void Exit()
    {
        // Reached turnpoint
        if (player.turner.HasReachedTurnPoint())
            player.SetState(player.inStreetState);
    }
}
