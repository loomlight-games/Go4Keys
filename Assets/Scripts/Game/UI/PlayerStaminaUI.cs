using UnityEngine.UI;

/// <summary>
/// Updates stamina bar as the player stamina
/// </summary>
public class PlayerStaminaUI
{
    readonly Slider staminaBar;

    public PlayerStaminaUI(Slider staminaBar)
    {
        this.staminaBar = staminaBar;
    }

    public void SubscribeToStaminaChangeEvent()
    {
        Player.Instance.resilient.StaminaChangeEvent += UpdateValue;
    }

    /// <summary>
    /// Updates bar/slider value to the received
    /// </summary>
    void UpdateValue(object sender, float stamina)
    {
        staminaBar.value = stamina;
    }
}
