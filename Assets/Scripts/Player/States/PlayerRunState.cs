
/// <summary>
/// Detects swipe up
/// </summary>
public class PlayerRunState : AState
{
    public override void Update()
    {
        Player.Instance.jumper.Update();
    }
}