using UnityEngine;

/// <summary>
/// Provides lateral movement between rails.
/// </summary>
public class RailControl
{
    readonly Transform player;
    readonly float sideSpeed;//Changing of rail
    readonly Transform railsParent;//Its children are the diferrent rails
    readonly float[] rails;//Array of X position of each rail
    Vector3 currentRailPos;//Vector of current rail position
    public int currentRailIndex = 1;//Index of current rail (in array)

    public RailControl(Transform player, float sideSpeed, Transform railsParent)
    {
        this.player = player;
        this.sideSpeed = sideSpeed;
        this.railsParent = railsParent;

        //Creates array the size of number of children of railsParent
        rails = new float[railsParent.childCount];

        //Fills the array with the rails positions
        for (int i = 0; i < rails.Length; i++)
        {
            rails[i] = railsParent.GetChild(i).position.x;
        }
    }

    public void Update()
    {
        Input();

        UpdateCurrentRail();

        MoveTowardsCurrentRail();
    }

    /// <summary>
    /// Handles inputs (move to left or right rail)
    /// </summary>
    void Input()
    {
        //A key pressed: change to left rail
        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            //Not in the leftMost rail
            if (currentRailIndex > 0)
            {
                //Move one rail to left
                currentRailIndex--;
            }
        }

        //D key pressed: change to right rail
        else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        {
            //Not in the rightMost rail
            if (currentRailIndex < rails.Length - 1)
            {
                //Move one rail to right
                currentRailIndex++;
            }
        }
    }

    /// <summary>
    /// Updates current rail vector with X position of the rail and Y and Z positions of this object
    /// </summary>
    private void UpdateCurrentRail()
    {
        currentRailPos = new Vector3(rails[currentRailIndex], player.localPosition.y, player.localPosition.z);
    }

    /// <summary>
    /// Moves to current rail at sideSpeed (according to local position = parent position).
    /// </summary>
    private void MoveTowardsCurrentRail()
    {
        //localposition adapts the axis of this object after the parent has rotated
        player.localPosition = Vector3.MoveTowards(player.localPosition, currentRailPos, sideSpeed * Time.deltaTime);
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     fill array with position in X axis of each waypoint
///     
/// every frame (update):
///     check if input to change rail is received
///     updates current rail
///     moves towards current rail position (X axis)
///     
