
/// <summary>
/// Can rotate to left if 'A' is pressed, or right if 'D'.
/// </summary>
public class PlayerAtIntersectionState : AState
{
    public override void Update()
    {
        Player.Instance.turner.Update(); // Can take a turn to another street
    }
}
