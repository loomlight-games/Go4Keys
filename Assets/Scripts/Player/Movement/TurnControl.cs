using UnityEngine;

/// <summary>
/// Provides turn ability to player parent at intersections.
/// </summary>
public class TurnControl
{
    readonly Transform playerParent;
    Vector2 position2;//Position (X,Z) of this object 
    Vector2 turnPoint;//Position or center (X, Z) of turn area 
    bool canTurn = false;//Can make a turn
    bool left = false;//Has turned left
    bool right = false;//has turned right
    //[SerializeField] AudioSource turningSound;

    public TurnControl(Transform playerParent)
    {
        this.playerParent = playerParent;
    }

    // Update is called once per frame
    public void Update()
    {
        //Saves X,Z position of this object
        position2 = new Vector2(playerParent.position.x, playerParent.position.z);

        //Has entered an intersection
        if (canTurn)
        {
            //Can make a turn
            CheckTurn();
        }
    }

    //Enters trigger area
    public void OnTriggerEnter(Collider other)
    {
        //With an intersection
        if (other.gameObject.CompareTag("Intersection"))
        {
            //Debug.Log("At intersection");

            //Saves intersection center (X,Z)
            turnPoint = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.z) ;

            //Debug.Log(turnPoint);

            //Can make a turn
            canTurn = true;
        }

    }

    //Turn to last selected side before reaching turnpoint
    void CheckTurn()
    {
        //The distance between this object and the turnpoint at least 0.4f
        if (Vector2.Distance(position2, turnPoint) <= 0.4f)
        {
            //Debug.Log("At turn point");

            //Turn to LEFT
            if (left)
            {
                CenterToTurnPoint();
                
                //Rotates object
                playerParent.Rotate(0f, -90f, 0f);

                //Can't make any more turn 
                canTurn = false;

                //turningSound.Play();

                //Debug.Log("Has rotated to left");
            }

            //Turn to RIGHT
            else if (right)
            {
                CenterToTurnPoint();

                //Rotates object
                playerParent.Rotate(0f, 90f, 0f);

                //Can't make any more turn 
                canTurn = false;

                //turningSound.Play();

                //Debug.Log("Has rotated to right");
            }

            //Has passed the turnpoint and continued forward
            else
            {
                //Debug.Log("Has continued forward");

                canTurn = false;
            }

        }
        //Not yet in turn point
        else
        {
            //Hasn't made a turn
            if (canTurn)
            {
                //A is pressed:Turn to LEFT
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //Debug.Log("A pressed");
                    left = true;
                    right = false;//The other side is neglected
                }
                //D is pressed: turn to RIGHT
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //Debug.Log("D pressed");
                    right = true;
                    left = false;//The other side is neglected
                }
            }
        }
    }

    /// <summary>
    /// Centers object to intersection center keeping Y position) to maintain intermediate rail at center of street
    /// </summary>
    void CenterToTurnPoint()
    {
        playerParent.position = new Vector3(turnPoint.x, playerParent.position.y, turnPoint.y);
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     
/// every frame (update):
///     updates position (X,Z) of this object
///     checks collision with turn area
///         if so, chooses side to turn before arriving to turn point (center of turn area)
///         centers its position to intersection center
///         rotates to that side at turn point