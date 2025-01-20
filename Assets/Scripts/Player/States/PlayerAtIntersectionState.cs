
/// <summary>
/// Can rotate to left if swipe left, or right if swipe right.
/// </summary>
public class PlayerAtIntersectionState : AState
{
    public override void Update()
    {
        Player.Instance.turner.Update(); // Can take a turn to another street
    }
}
