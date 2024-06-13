using System;

/// <summary>
/// Player has been caught. No return.
/// </summary>
public class CaughtState : APlayerState
{
    public override void Enter(AStateController controller)
    {
        player = (Player) controller;
    }
}
