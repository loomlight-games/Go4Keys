using UnityEngine;

/// <summary>
/// Runs forward endlessly, losing stamina, and changes rail.
/// </summary>
public class InStreetState : APlayerState
{
    bool atIntersection = false;

    public override void Enter(AStateController controller)
    {
        player = (Player) controller;
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly,
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Change rails
        
        Exit(); // Checks exit
    }

    public override void OnTriggerEnter(Collider other) 
    {
        player.resilient.OnTriggerEnter(other);
        player.collecter.OnTriggerEnter(other);
        player.turner.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Intersection"))
            atIntersection = true;
    }

    public override void Exit()
    {
        // 'Space' key pressed 
        if (Input.GetKeyDown(KeyCode.Space))
            player.SetState(player.jumpState);

        // If entered an intersection
        if (atIntersection)
            player.SetState(player.atIntersection);
            atIntersection = false;
    }
}