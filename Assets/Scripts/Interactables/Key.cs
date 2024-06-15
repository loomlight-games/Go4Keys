using UnityEngine;

/// <summary>
/// Key properties and behaviour
/// </summary>
public class Key : MonoBehaviour
{
    public RotatorySO rotationType;

    // Update is called once per frame
    void Update()
    {
        // Rotates
        transform.Rotate(360 * rotationType.Xspeed * Time.deltaTime, 
                         360 * rotationType.Yspeed * Time.deltaTime, 
                         360 * rotationType.Zspeed * Time.deltaTime);
    }
}
