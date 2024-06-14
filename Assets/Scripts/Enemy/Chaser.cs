using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Provides chaser behaviour after the player. Jump obstacles automatically.
/// </summary>
public class Chaser : MonoBehaviour
{
    Rigidbody chaserRigidBody;
    Player player; // Target
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

        player = Player.Instance;
        player.chased.CaughtEvent += TargetCaught;
        player.chased.ChaserResettedEvent += ResetPosition;
    }

    void Update()
    {
        // Replicates target position in X
        transform.localPosition = new Vector3(player.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        if (!targetCaught)
        {
            //Obstacle in front of target (has stopped)
            if (Physics.CheckSphere(player.obstacleChecker.position, .4f, player.obstacleLayer)) //Same radious as EndlessRunner.CheckObstacle
            {
                targetRunning = false;
                transform.Translate(acceleration*accelerationIncrement * Time.deltaTime * transform.forward, Space.World);
            }
            else //No obstacle in front (target still running)
            {
                targetRunning = true;
                transform.Translate(acceleration * Time.deltaTime * transform.forward, Space.World);
            }

            // Check for obstacles and jump if it's also touching ground
            if (Physics.CheckSphere(checker.position, checkerRadious, player.obstacleLayer)
                && Physics.CheckSphere(checker.position, checkerRadious, player.groundLayer))
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
        transform.localPosition = new Vector3(player.transform.localPosition.x, 
                                              transform.localPosition.y, 
                                              player.transform.localPosition.z - resetDistance);
    }
}