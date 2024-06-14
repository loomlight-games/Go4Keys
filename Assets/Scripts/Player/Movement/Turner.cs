using System;
using UnityEngine;

/// <summary>
/// Provides turn ability at intersections. Rotates from intersection center (turn point).
/// </summary>
public class Turner
{
    public event EventHandler<bool> TurnedEvent;

    readonly Transform turner;
    Vector2 transformPosition2D;
    Vector2 turnPoint2D;
    /// <summary>
    /// -1 Left 0 Forward 1 Right
    /// </summary>
    int turn = 0;

    public Turner(Transform transform)
    {
        turner = transform;
    }

    public void Update()
    {
        // Saves X,Z position of this object
        transformPosition2D = new Vector2(turner.position.x, turner.position.z);

        // Not yet at turn point (with some threshold)
        if (Vector2.Distance(transformPosition2D, turnPoint2D) > 0.4f)
        {
            if (Input.GetKeyDown(KeyCode.A)) // To left
            {
                turn = -1;
            }
            if (Input.GetKeyDown(KeyCode.D)) // To right
            {
                turn = 1;
            }
        }
        else // Has reached turn point
        {
            // Centers object to intersection center to maintain intermediate rail at center of street
            turner.position = new Vector3(turnPoint2D.x, turner.position.y, turnPoint2D.y);

            if (turn == -1)
            {
                turner.Rotate(0f, -90f, 0f);
                TurnedEvent?.Invoke(this, true);
            }
            else if (turn == 1)
            {
                turner.Rotate(0f, 90f, 0f);
                TurnedEvent?.Invoke(this, true);
            }

            // Reset decision
            turn = 0;

            TurnedEvent?.Invoke(this, false);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //With an intersection
        if (other.gameObject.CompareTag("Intersection"))
        {
            // Intersection center = turn point
            turnPoint2D = new Vector2(other.gameObject.transform.position.x, 
                                    other.gameObject.transform.position.z) ;
        }
    }
}