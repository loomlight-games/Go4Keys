using System;

/// <summary>
/// Player has been caught. Invokes the corresponding event.
/// </summary>
public class CaughtState : APlayerState
{
    public event EventHandler CaughtEvent;

    public override void Enter(AStateController controller)
    {
        player = (Player) controller;

        CaughtEvent?.Invoke(this, EventArgs.Empty);
    }
}
