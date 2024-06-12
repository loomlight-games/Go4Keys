using UnityEngine;

/// <summary>
/// Jumps once while still moving forward endlessly and changing rail
/// </summary>
public class JumpState : APlayerState
{
    //AudioSource jumpSound;

    public override void Enter(AStateController controller)
    {
        player = (Player) controller;
        player.jumper.Jump();// Jumps
        player.resilient.Jumps();// Jumped, thus, loses a lot of stamina
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Continues moving forward
        player.railed.Update(); // Change rails
        
        Exit(); // Checks exit
    }

    public override void OnTriggerEnter(Collider other)
    {
        player.resilient.OnTriggerEnter(other);
        player.collecter.OnTriggerEnter(other);
    }

    public override void Exit()
    {
        // Has fallen to ground
        if (player.jumper.IsGrounded()) 
            player.SetState(player.inStreetState);
    }
}
