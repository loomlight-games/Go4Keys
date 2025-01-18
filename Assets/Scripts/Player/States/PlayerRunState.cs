
/// <summary>
/// Detects if 'Space' is pressed in any moment.
/// </summary>
public class PlayerRunState : AState
{
    public override void Update()
    {
        Player.Instance.jumper.Update();
    }
}