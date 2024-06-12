using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    StaminaSystem staminaSystem;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        staminaSystem = Player.Instance.staminaSystem;

        //SUBSCRIBES ENDRESULT TO EVENT HANDLER OF RESULT
        staminaSystem.StaminaChangeEvent += SetStamina;
    }

    public void SetStamina(object sender, float stamina)
    {
        slider.value = stamina;
    }
}
