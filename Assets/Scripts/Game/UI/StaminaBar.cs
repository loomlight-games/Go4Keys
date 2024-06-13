
using UnityEngine.UI;

/// <summary>
/// Updates stamina bar as the player stamina
/// </summary>
public class StaminaBar
{
    readonly Slider staminaBar;

    public StaminaBar(Slider staminaBar)
    {
        this.staminaBar = staminaBar;
    }

    public void Start()
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
