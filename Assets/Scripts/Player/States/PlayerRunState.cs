
/// <summary>
/// Detects if 'Space' is pressed in any moment.
/// </summary>
public class PlayerRunState : APlayerState
{
    public override void Update()
    {
        player.jumper.Update(); // Detects if space is pressed
    }
}