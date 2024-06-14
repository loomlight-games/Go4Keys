using System;

/// <summary>
/// Player has been caught. No return.
/// </summary>
public class PlayerEndState : APlayerState
{
    public event EventHandler<string> EndGameEvent;

    public override void Enter(string result)
    {
        EndGameEvent?.Invoke(this, result);
    }
}
