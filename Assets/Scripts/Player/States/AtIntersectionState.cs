using UnityEngine;

/// <summary>
/// Takes a turn at an intersection while still moving forward endlessly and changing rail
/// </summary>
public class AtIntersectionState : APlayerState
{
    bool caught = false;

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

        Exit();
    }
    public override void OnTriggerEnter(Collider other)
    {
        player.resilient.OnTriggerEnter(other);
        player.collecter.OnTriggerEnter(other);
        player.turner.OnTriggerEnter(other);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            caught = true;
    }

    public override void Exit()
    {
        if (player.turner.HasReachedTurnPoint())
            player.SetState(player.runState);
        else if (caught)
            player.SetState(player.caughtState);
    }
}
