using UnityEngine;

/// <summary>
/// Defines the type of energy drink
/// </summary>
[CreateAssetMenu(fileName = "EnergyDrink", menuName = "GameItems/EnergyDrink", order = 0)]
public class EnergyDrinkSO : ScriptableObject
{
    public string type;
}
