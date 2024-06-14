using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Provides chaser behaviour after the player. Jump obstacles automatically.
/// </summary>
public class Chaser : MonoBehaviour
{
    Rigidbody chaserRigidBody;
    Transform playerObstacleChecker;
    Transform playerPosition;
    bool targetCaught = false;
    bool targetRunning = false;

    [SerializeField] float acceleration; // Related to its parent speed
    [SerializeField] float accelerationIncrement;
    [SerializeField] float jumpForce;
    [SerializeField] Transform checker;
    [SerializeField] float checkerRadious;

    void Start()
    {
        chaserRigidBody = transform.GetComponent<Rigidbody>();

        Player.Instance.chased.CaughtEvent += TargetCaught;
        Player.Instance.chased.ChaserResettedEvent += ResetPosition;
        playerPosition = Player.Instance.transform;
        playerObstacleChecker = GameObject.Find("Player obstacle checker").transform;
    }

    void Update()
    {
        // Replicates target position in X
        transform.localPosition = new Vector3(playerPosition.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        if (!targetCaught)
        {
            //Obstacle (7) in front of target (has stopped)
            if (Physics.CheckSphere(playerObstacleChecker.position, .4f, 7)) //Same radious as EndlessRunner.CheckObstacle
            {
                targetRunning = false;
                transform.Translate(acceleration*accelerationIncrement * Time.deltaTime * transform.forward, Space.World);
            }
            else //No obstacle in front (target still running)
            {
                targetRunning = true;
                transform.Translate(acceleration * Time.deltaTime * transform.forward, Space.World);
            }

            // Check for obstacles (7) and jump if it's also touching ground (3)
            if (Physics.CheckSphere(checker.position, checkerRadious, 7)
                && Physics.CheckSphere(checker.position, checkerRadious, 3))
            {
                if (targetRunning) //Jumps if target is running
                {
                    chaserRigidBody.velocity = new Vector3(chaserRigidBody.velocity.x, jumpForce, chaserRigidBody.velocity.z);
                }
            }
        }
    }

    /// <summary>
    /// Called by the event invoked when target has been caught.
    /// </summary>
    void TargetCaught(object sender, EventArgs e)
    {
        targetCaught = true;
    }

    /// <summary>
    /// Called by the event invoked when target collides with a resetter.
    /// </summary>
    void ResetPosition(object sender, float resetDistance)
    {
        transform.localPosition = new Vector3(playerPosition.localPosition.x, 
                                              transform.localPosition.y,
                                              playerPosition.localPosition.z - resetDistance);
    }
}