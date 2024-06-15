using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates stamina bar as the player stamina
/// </summary>
public class PlayerStaminaUI
{
    Slider staminaBar;

    public void Initialize()
    {
        staminaBar = GameObject.Find("Stamina bar").GetComponent<Slider>();

        PlayerManager.Instance.resilient.StaminaChangeEvent += UpdateValue;
    }

    /// <summary>
    /// Updates bar/slider value to the received
    /// </summary>
    void UpdateValue(object sender, float stamina)
    {
        staminaBar.value = stamina;
    }
}
