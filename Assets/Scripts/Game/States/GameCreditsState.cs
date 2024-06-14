
public class GameCreditsState : AGameState
{
    string buttonClickedName = "None";
    bool eventsSubscribed = false;

    public override void Enter()
    {
        if (!eventsSubscribed) // Subscribes to events just once
        {
            game.ButtonClicked += ButtonClicked;
            eventsSubscribed = true;
        }
    }

    public override void Exit()
    {
        switch (buttonClickedName)
        {
            case "Return":
                game.SetState(game.mainMenuState);
                break;
            default:
                break;
        }
    }

    void ButtonClicked(object sender, string buttonName)
    {
        this.buttonClickedName = buttonName;
    }
}