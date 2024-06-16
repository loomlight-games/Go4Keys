using System;
using UnityEngine;

/// <summary>
/// Engrgy drink properties and behaviour.
/// </summary>
public class EnergyDrink : MonoBehaviour
{

    #region INTRINSIC STATE
    // Common properties for all the energy drinks of the same type
    // (Defined in the prefab)
    public EnergyDrinkType type;
    public RotatorySO rotationType;
    Transform upMark;
    Transform downMark;
    #endregion

    #region EXTRINSIC STATE 
    // Differente in each object
    readonly System.Random random = new();
    [HideInInspector] public float recoverValue;
    bool goUp = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Assert the tag
        if (!CompareTag("EnergyDrink")) tag = "EnergyDrink";

        // Find marks
        upMark = transform.parent.Find("Up");
        downMark = transform.parent.Find("Down");

        recoverValue = (float)random.Next(type.minValue, type.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates
        transform.Rotate(360 * rotationType.Xspeed * Time.deltaTime,
                         360 * rotationType.Yspeed * Time.deltaTime,
                         360 * rotationType.Zspeed * Time.deltaTime);

        // Bounces
        if (goUp) MoveTo(upMark); 
        else MoveTo(downMark);

        // Reaches an extreme (with a threshold)
        if (Math.Abs(transform.position.y - downMark.position.y) <= 0.01f 
         || Math.Abs(transform.position.y - upMark.position.y) <= 0.01f)
            goUp = !goUp; //Goes to the other
    }

    private void MoveTo(Transform mark)
    {
        transform.position = Vector3.MoveTowards(transform.position, mark.position, type.bounceSpeed * Time.deltaTime);
    }
}
