using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //SUBJECT
    [SerializeField] StaminaSystem stamina;

    //Slider
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //SUBSCRIBES ENDRESULT TO EVENT HANDLER OF RESULT
        stamina.StaminaChangeEvent += SetStamina;
    }

    public void SetStamina(object sender, float stamina)
    {
        slider.value = stamina;
    }
}
