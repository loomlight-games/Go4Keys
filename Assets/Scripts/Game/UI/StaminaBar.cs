
using UnityEngine.UI;

public class StaminaBar// : MonoBehaviour
{
    Slider staminaBar;

    public StaminaBar(Slider staminaBar)
    {
        this.staminaBar = staminaBar;
    }

    // Start is called before the first frame update
    public void Start()
    {
        Player.Instance.resilient.StaminaChangeEvent += SetStamina;
    }

    void SetStamina(object sender, float stamina)
    {
        staminaBar.value = stamina;
    }
}
