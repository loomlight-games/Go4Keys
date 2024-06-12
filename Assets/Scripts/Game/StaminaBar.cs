using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Resilient staminaSystem;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        staminaSystem = Player.Instance.resilient;

        //SUBSCRIBES ENDRESULT TO EVENT HANDLER OF RESULT
        staminaSystem.StaminaChangeEvent += SetStamina;
    }

    public void SetStamina(object sender, float stamina)
    {
        slider.value = stamina;
    }
}
