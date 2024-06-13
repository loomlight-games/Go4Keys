using System;
using UnityEngine;

/// <summary>
/// Provides limited stamina.
/// </summary>
public class Resilient
{
    public event EventHandler TiredEvent;//Stores methods to invoke when there's no stamina left
    public event EventHandler <float> StaminaChangeEvent;//Stores methods to invoke when stamina changes
    
    readonly float lossPerStep;
    readonly float lossPerJump;
    const float MAX_STAMINA = 100;
    float currentStamina;
    bool staminaAlreadyEnded = false;

    //[SerializeField] AudioSource drinkSound;//Audio

    public Resilient(float staminaLossPerStep, float staminaLossPerJump)
    {
        this.lossPerStep = staminaLossPerStep;
        this.lossPerJump = staminaLossPerJump;

        currentStamina = MAX_STAMINA;
    }

    /// <summary>
    /// Called when running (every frame)
    /// </summary>
    public void Runs()
    {
        currentStamina -= lossPerStep * Time.deltaTime;
        CheckEvents();
    }

    /// <summary>
    /// Called when jumping (once)
    /// </summary>
    public void Jumps()
    {
        currentStamina -= lossPerJump * Time.deltaTime;
        CheckEvents();
    }

    /// <summary>
    /// Invokes events when stamina changes or there isn't any left
    /// </summary>
    public void CheckEvents()
    {
        //Has enough stamina
        if (currentStamina > 0)
        {
            //Invokes methods in eventHandler
            StaminaChangeEvent?.Invoke(this, currentStamina);
        }
        //Doesn't have any more stamina
        else
        {
            if (!staminaAlreadyEnded)
            {
                //Invokes methods in eventHandler
                TiredEvent?.Invoke(this, EventArgs.Empty);

                //To ensure that TiredEvent is only invoked once
                //(if not: loss sound won't stop playing)
                staminaAlreadyEnded = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
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
                currentStamina = MAX_STAMINA;

            //Audio
            //drinkSound.Play();
        }
    }
}