
/// <summary>
/// Can rotate to left if 'A' is pressed, or right if 'D'.
/// </summary>
public class PlayerAtIntersectionState : APlayerState
{
    public override void Update()
    {
        player.turner.Update(); // Can take a turn to another street
    }
}
