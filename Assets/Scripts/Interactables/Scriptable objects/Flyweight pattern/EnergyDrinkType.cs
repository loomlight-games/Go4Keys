using UnityEngine;

/// <summary>
/// Defines the type of energy drink
/// </summary>
[CreateAssetMenu(fileName = "EnergyDrink", menuName = "GameItems/EnergyDrink", order = 0)]
public class EnergyDrinkType : ScriptableObject
{
    public int minValue, maxValue;
    public float bounceSpeed;
}
