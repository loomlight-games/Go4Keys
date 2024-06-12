using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates new tab in Create menu
[CreateAssetMenu(fileName = "EnergyDrink", menuName = "GameItems/EnergyDrink", order = 0)]

//DEFINES INTRINSIC PROPERTIES OF ENERGY DRINKS (FLYWEIGHT)
public class EnergyDrinkSO : ScriptableObject
{
    // Resistance recovered
    public float recover;
}
