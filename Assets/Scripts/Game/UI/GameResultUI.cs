using UnityEngine;

/// <summary>
/// Handles game result pop ups.
/// </summary>
public class GameResultUI
{
    readonly GameObject victory;
    readonly GameObject caught;
    readonly GameObject tired;

    public GameResultUI(GameObject victory, GameObject caught, GameObject tired)
    {
        this.victory = victory;
        this.caught = caught;
        this.tired = tired;
    }

    public void HideAll()
    {
        victory.SetActive(false);
        caught.SetActive(false);
        tired.SetActive(false);
    }

    public void ShowVictory()
    {
        victory.SetActive(true);
    }

    public void ShowCaught()
    {
        caught.SetActive(true);
    }

    public void ShowTired()
    {
        tired.SetActive(true);
    }
}
