using System;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    //EVENT HANDLERS
    public event EventHandler TiredEvent;//Stores methods to invoke when there's no stamina left
    public event EventHandler <float> StaminaChangeEvent;//Stores methods to invoke when stamina changes

    //Damage
    [SerializeField] Transform obstacleChecker;
    [SerializeField] LayerMask damageLayer;

    //Values of change
    [SerializeField] float lossPerSecond = 2f;
    [SerializeField] float lossPerCrash = 5f;

    //Audio
    [SerializeField] AudioSource drinkSound;

    //Float
    private float maxStamina = 100;
    private float stamina;

    //Bool
    bool endedStamina = false;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Has enough stamina
        if (stamina > 0)
        {
            CheckObstacle();
        }
        //Doesn't have any more stamina
        else
        {
            if (!endedStamina)
            {
                //Invokes methods in eventHandler
                TiredEvent?.Invoke(this, EventArgs.Empty);

                //To ensure that TiredEvent is only invoked once
                //(if not: loss sound won't stop playing)
                endedStamina = true;
            }
        }

        //Invokes methods in eventHandler
        StaminaChangeEvent?.Invoke(this, stamina);
    }

    private void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the damage layer
        //Has hit an damaging object
        if (Physics.CheckSphere(obstacleChecker.position, .3f, damageLayer))
        {
            //Loses stamina on crash
            LoseStamina(lossPerCrash);
        }
        //No obstacle in front
        else
        {
            //Reduces stamina every second
            LoseStamina(lossPerSecond);
        }
    }

    private void LoseStamina(float value)
    {
        stamina -= value * Time.deltaTime;
    }

    //Collision trigger
    private void OnTriggerEnter(Collider other)//works same as onCollisionEnter
    {
        //With an energy drink
        if (other.gameObject.CompareTag("EnergyDrink"))
        {
            EnergyDrink energyDrink = other.GetComponent<EnergyDrink>();

            stamina += energyDrink.recoverAmount.recover;

            //Destroys it
            Destroy(other.gameObject);

            //Ensures stamina max
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }

            //Audio
            drinkSound.Play();
        }
    }
}




