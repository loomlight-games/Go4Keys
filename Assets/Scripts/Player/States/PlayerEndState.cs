using System;

/// <summary>
/// Player has been caught, is tired or has found all collectibles. No exit.
/// </summary>
public class PlayerEndState : AState
{
    public event EventHandler<string> EndGameEvent;

    public override void Enter(string result)
    {
        EndGameEvent?.Invoke(this, result);
    }
}
