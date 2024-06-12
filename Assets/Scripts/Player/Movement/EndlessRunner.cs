using UnityEngine;

/// <summary>
/// Provides jump ability to player and endless forward movement to its parent. Stops when detecting an obstacle in front.
/// </summary>
public class EndlessRunner
{
    readonly Rigidbody playerRigidBody;
    readonly Transform playerParent;
    readonly float forwardSpeed;
    float forwardValue;
    readonly float jumpForce;
    readonly Transform groundChecker;
    readonly Transform obstacleChecker;
    readonly LayerMask jumpLayer;//Only jump when touching this layer
    readonly LayerMask stopLayer;//Stops when touching this layer

    //Audio
    //AudioSource jumpSound;

    public EndlessRunner(Rigidbody playerRigidbody, Transform playerParent, float forwardSpeed, float jumpForce, Transform groundChecker, Transform obstacleChecker, LayerMask jumpLayer, LayerMask stopLayer)
    {
        this.playerRigidBody = playerRigidbody;
        this.playerParent = playerParent;
        this.forwardSpeed = forwardSpeed;
        this.jumpForce = jumpForce;
        this.groundChecker = groundChecker;
        this.obstacleChecker = obstacleChecker;
        this.jumpLayer = jumpLayer;
        this.stopLayer = stopLayer;
    }

    public void Update()
    {
        MoveForward();
        Input();
    }

    /// <summary>
    /// Move forward automatically if possible (no obstacle in front)
    /// </summary>
    private void MoveForward()
    {
        //Will update forward speed accordingly
        CheckObstacle();

        //Move the object at forward speed to the orientation it's facing
        playerParent.Translate(forwardValue * Time.deltaTime * Vector3.forward);

    }

    /// <summary>
    /// Check for obstacles in front
    /// </summary>
    private void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the stop layer (obstacle)
        if (Physics.CheckSphere(obstacleChecker.position, .4f, stopLayer))//Same radious as Chaser.UpdateChaseSpeed
        {
            //Debug.Log("Player has stopped");

            //Stop moving
            forwardValue = 0.0f;
        }
        else
        {
            //Resume moving
            forwardValue = forwardSpeed;
        }
    }

    /// <summary>
    /// Handles inputs (jump)
    /// </summary>
    private void Input()
    {
        //Space key pressed 
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && CheckGround())//Jumps if Space is pressed AND it's on floor
        {
            //Calls jump function
            Jump();
        }
    }

    /// <summary>
    /// Moves the player in Y axis to jump height
    /// </summary>
    public void Jump()
    {
        //Maintains velocities in x and z axis but increments the y with jumpforce
        playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, jumpForce, playerRigidBody.velocity.z);
        //jumpSound.Play();

        //Debug.Log("Player jumped");
    }

    /// <summary>
    /// Check it's touching ground with checker
    /// </summary>
    /// <returns></returns>
    bool CheckGround()
    {
        //Creates a small sphere in feet position that detects the floor layer
        return Physics.CheckSphere(groundChecker.position, 0.2f, jumpLayer);
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     get rigid body
/// every frame (update):
///     check if there's an obstacle in front
///         if so, stop
///         it not so, move parent forwards
///     jump if key pressed and on floor
/// 