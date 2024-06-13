public class InGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        game.stamina.Start();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }


}