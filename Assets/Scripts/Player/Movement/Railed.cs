using System;
using UnityEngine;

/// <summary>
/// Provides lateral movement between rails.
/// </summary>
public class Railed
{
    readonly Transform player;
    readonly float railChangeSpeed;
    readonly float[] railsXPositions;
    public int currentRailIndex = 1;
    Vector3 currentRailPos;

    public Railed(Transform player, float railChangeSpeed, Transform railsParent)
    {
        this.player = player;
        this.railChangeSpeed = railChangeSpeed;

        //Creates array the size of number of children of railsParent
        railsXPositions = new float[railsParent.childCount];

        //Fills the array with the rails positions
        for (int i = 0; i < railsXPositions.Length; i++)
        {
            railsXPositions[i] = railsParent.GetChild(i).position.x;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // To left rail
        { 
            //Not in the leftMost rail
            if (currentRailIndex > 0)
            {
                //Move one rail to left
                currentRailIndex--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D)) // To right rail
        {
            //Not in the rightMost rail
            if (currentRailIndex < railsXPositions.Length - 1)
            {
                //Move one rail to right
                currentRailIndex++;
            }
        }

        // Updates current rail vector with X position of the rail and Y and Z positions of player
        currentRailPos = new Vector3(railsXPositions[currentRailIndex], 
                                    player.localPosition.y, 
                                    player.localPosition.z);

        // Moves to current rail (according to local position = parent position).
        // localposition adapts the axis of this object after the parent has rotated
        player.localPosition = Vector3.MoveTowards(player.localPosition, currentRailPos, railChangeSpeed * Time.deltaTime);
    }
}    
