

public class PlayerStateController : AStateController
{
    IState currentState;

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public override IState GetState()
    {
        return currentState;
    }

    public override void SetState(IState state)
    {
        currentState = state;
    }
}
