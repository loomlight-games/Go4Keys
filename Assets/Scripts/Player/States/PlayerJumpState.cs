
/// <summary>
/// Jumps once, losing  a lot of stamina, and checks if is grounded again.
/// </summary>
public class PlayerJumpState : APlayerState
{
    public override void Enter()
    {
        player.jumper.Jump();// Jumps,
        player.resilient.LossPerJump();//thus, loses a lot of stamina
    }

    public override void Update()
    {
        player.jumper.IsGrounded(); // Checks if is grounded
    }
}
