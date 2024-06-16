
/// <summary>
/// Jumps once, losing  a lot of stamina, and checks if is grounded again.
/// </summary>
public class PlayerJumpState : AState
{
    public override void Enter()
    {
        Player.Instance.jumper.Jump();// Jumps,
        Player.Instance.resilient.LossPerJump();//thus, loses a lot of stamina
    }

    public override void Update()
    {
        Player.Instance.jumper.IsGrounded(); // Checks if is grounded
    }
}
