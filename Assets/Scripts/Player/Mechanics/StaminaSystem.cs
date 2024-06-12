using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Provides limited stamina.
/// </summary>
public class StaminaSystem
{
    public event EventHandler TiredEvent;//Stores methods to invoke when there's no stamina left
    public event EventHandler <float> StaminaChangeEvent;//Stores methods to invoke when stamina changes
    readonly Transform obstacleChecker;
    readonly LayerMask obstacleLayer;
    readonly float lossPerSecond;
    readonly float lossPerCrash;
    const float MAX_STAMINA = 100;
    float currentStamina;
    bool stamineEndedOnce = false;
    //[SerializeField] AudioSource drinkSound;//Audio

    public StaminaSystem(Transform obstacleChecker, LayerMask obstacleLayer, float staminaLossPerSecond, float staminaLossPerCrash)
    {
        this.obstacleChecker = obstacleChecker;
        this.obstacleLayer = obstacleLayer;
        this.lossPerSecond = staminaLossPerSecond;
        this.lossPerCrash = staminaLossPerCrash;

        currentStamina = MAX_STAMINA;
    }

    // Update is called once per frame
    public void Update()
    {
        //Has enough stamina
        if (currentStamina > 0)
        {
            CheckObstacle();
        }
        //Doesn't have any more stamina
        else
        {
            if (!stamineEndedOnce)
            {
                //Invokes methods in eventHandler
                TiredEvent?.Invoke(this, EventArgs.Empty);

                //To ensure that TiredEvent is only invoked once
                //(if not: loss sound won't stop playing)
                stamineEndedOnce = true;
            }
        }

        //Invokes methods in eventHandler
        StaminaChangeEvent?.Invoke(this, currentStamina);
    }

    //Collision trigger
    public void OnTriggerEnter(Collider other)//works same as onCollisionEnter
    {
        //With an energy drink
        if (other.gameObject.CompareTag("EnergyDrink"))
        {
            EnergyDrink energyDrink = other.GetComponent<EnergyDrink>();

            currentStamina += energyDrink.recoverAmount.recover;

            //Deactivates it
            other.gameObject.SetActive(false);

            //Ensures stamina max
            if (currentStamina > MAX_STAMINA)
            {
                currentStamina = MAX_STAMINA;
            }

            //Audio
            //drinkSound.Play();
        }
    }

    void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the damage layer
        //Has hit an damaging object
        if (Physics.CheckSphere(obstacleChecker.position, .3f, obstacleLayer))
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

    void LoseStamina(float value)
    {
        currentStamina -= value * Time.deltaTime;
    }
}




