using System;
using UnityEngine;

/// <summary>
/// Provides limited stamina. Can recover it when hitting certain object.
/// </summary>
public class Resilient
{
    public event EventHandler <float> StaminaChangeEvent;

    readonly float lossPerStep;
    readonly float lossPerJump;
    const float MAX_STAMINA = 100;
    float currentStamina;

    public Resilient(float staminaLossPerStep, float staminaLossPerJump)
    {
        // Always negative
        this.lossPerStep = -Math.Abs(staminaLossPerStep);
        this.lossPerJump = -Math.Abs(staminaLossPerJump);

        currentStamina = MAX_STAMINA;
    }

    /// <summary>
    /// Called when running (every frame)
    /// </summary>
    public void Runs()
    {
        currentStamina += lossPerStep * Time.deltaTime;

        InvokeEvent();
    }

    /// <summary>
    /// Called when jumping (once)
    /// </summary>
    public void Jumps()
    {
        currentStamina += lossPerJump * Time.deltaTime;

        InvokeEvent();
    }

    public void OnTriggerEnter(Collider other)
    {
        //With an energy drink
        if (other.gameObject.CompareTag("EnergyDrink"))
        {
            currentStamina += other.GetComponent<EnergyDrink>().type.recover;

            //Deactivates it
            other.gameObject.SetActive(false);

            InvokeEvent();
        }
    }

    void InvokeEvent()
    {
        // Ensures stamina max
        if (currentStamina > MAX_STAMINA)
            currentStamina = MAX_STAMINA;

        StaminaChangeEvent?.Invoke(this, currentStamina);
    }
}