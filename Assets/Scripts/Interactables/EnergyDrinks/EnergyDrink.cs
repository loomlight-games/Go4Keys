using UnityEngine;

//ENERGY DRINK PROPERTIES AND BEHAVIOUR

public class EnergyDrink : MonoBehaviour
{
    // Intrinsic properties (FLYWEIGHT)
    // Type of energy drink 
    public EnergyDrinkSO type;

    //Extrinsic properties
    public RotatorySO rotationType;

    // Start is called before the first frame update
    void Start()
    {
        // Assert the tag
        if (!CompareTag("EnergyDrink"))
            this.tag = "EnergyDrink";
    }

    // Update is called once per frame
    void Update()
    {
        //ROTATION
        //360 degrees * speed * time independent from framerate
        //speed = 1 is a whole rotation
        transform.Rotate(360 * rotationType.Xspeed * Time.deltaTime,
            360 * rotationType.Yspeed * Time.deltaTime,
            360 * rotationType.Zspeed * Time.deltaTime);
    }
}
