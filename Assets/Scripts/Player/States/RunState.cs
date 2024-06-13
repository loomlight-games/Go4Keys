using UnityEngine;

/// <summary>
/// Runs forward endlessly, losing stamina and changing rail.
/// </summary>
public class RunState : APlayerState
{
    bool atIntersection = false;
    bool caught = false;

    public override void Enter(AStateController controller)
    {
        player = (Player) controller;
    }

    public override void Update()
    {
        player.endlessRunner.Update(); // Runs endlessly,
        player.resilient.Runs(); // thus, loses stamina
        player.railed.Update(); // Change rails
        
        Exit();
    }

    public override void OnTriggerEnter(Collider other) 
    {
        player.resilient.OnTriggerEnter(other);
        player.collecter.OnTriggerEnter(other);
        player.turner.OnTriggerEnter(other);
        player.chaserResetter.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Intersection"))
            atIntersection = true;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            caught = true;
    }

    public override void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(player.jumpState);
        }
        else if (atIntersection)
        {
            player.SetState(player.atIntersection);
            atIntersection = false;
        }
        else if (caught)
        {
            player.SetState(player.caughtState);
        }
    }
}