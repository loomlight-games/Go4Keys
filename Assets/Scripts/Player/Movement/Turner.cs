using System;
using UnityEngine;

/// <summary>
/// Provides turn ability at intersections.
/// </summary>
public class Turner
{
    readonly Transform transform;
    Vector2 transformPosition2D;
    Vector2 turnPoint2D;
    bool turnPointReached = false;
    bool turnLeft = false;
    bool turnRight = false;

    public Turner(Transform transform)
    {
        this.transform = transform;
    }

    public void Update()
    {
        // Hasn´t reached turnPoint yet
        if (!turnPointReached)
        {
            // Saves X,Z position of this object
            transformPosition2D = new Vector2(transform.position.x, transform.position.z);

            // Not yet at turn point (with some threshold)
            if (Vector2.Distance(transformPosition2D, turnPoint2D) > 0.4f)
            {
                if (Input.GetKeyDown(KeyCode.A)) // To left
                {
                    turnLeft = true;
                    turnRight = false; //The other side is neglected
                }
                if (Input.GetKeyDown(KeyCode.D)) // To right
                {
                    turnRight = true;
                    turnLeft = false;
                }
            }
            else // Has reached turn point
            {
                // Centers object to intersection center to maintain intermediate rail at center of street
                transform.position = new Vector3(turnPoint2D.x, transform.position.y, turnPoint2D.y);

                if (turnLeft)
                    transform.Rotate(0f, -90f, 0f);
                else if (turnRight)
                    transform.Rotate(0f, 90f, 0f);

                turnPointReached = true;
            }
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

            turnPointReached = false;
        }
    }

    internal bool HasReachedTurnPoint()
    {
        return turnPointReached;
    }
}