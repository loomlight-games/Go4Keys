public class InGameState : AGameState
{
    public override void Enter(AStateController controller)
    {
        game = (GameManager)controller;

        game.stamina.Start();
        game.collectibles.Start();
        game.autosave.Start();
    }

    public override void Update()
    {
        game.autosave.Update();
    }

    public override void Exit()
    {
        
    }


}