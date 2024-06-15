using System;
using UnityEngine;

/// <summary>
/// Provides chaser behaviour after the player. Jump obstacles automatically.
/// </summary>
public class Chaser : MonoBehaviour
{
    Rigidbody chaserRigidBody;
    Transform robberObstacleChecker;
    Transform player;
    LayerMask obstacle;
    LayerMask ground;
    bool targetCaught = false;
    bool targetStopped = false;
    float lastZposition = 100f;
    float correspondingPosition, normalPosition, fastPosition, farthestPos;

    [SerializeField] float acceleration; // Related to its parent speed
    [SerializeField] float accelerationIncrement;
    [SerializeField] float jumpForce;
    [SerializeField] float checkerRadious;

    void Start()
    {
        Player.Instance.endlessRunner.ObstacleInFront += TargetStopped;
        Player.Instance.chased.CaughtEvent += TargetCaught;
        Player.Instance.chased.ChaserResettedEvent += ResetPosition;

        player = Player.Instance.transform;

        chaserRigidBody = transform.GetComponent<Rigidbody>();
        robberObstacleChecker = GameObject.Find("Robber checker").transform;
        obstacle = 1 << LayerMask.NameToLayer("Obstacle");
        ground = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        if (!targetCaught)
        {
            // Last position at max speed
            correspondingPosition = lastZposition + acceleration * accelerationIncrement * Time.deltaTime;

            // Has advanced too fast from last position
            if (transform.localPosition.z > correspondingPosition) 
            {
                // Resets to last position at max speed
                UpdatePos(correspondingPosition); 
            }
            // It's too much behind the player
            else if (transform.localPosition.z < player.localPosition.z - 8f)
            {
                farthestPos = player.localPosition.z - 8f + acceleration * accelerationIncrement * Time.deltaTime;

                // Resets to farthest position
                UpdatePos(farthestPos);
            }
            // Moves normally
            else
            {
                // Current position at normal speed
                normalPosition = transform.localPosition.z + acceleration * Time.deltaTime;
                // Current position at max speed
                fastPosition = transform.localPosition.z + acceleration * accelerationIncrement * Time.deltaTime;

                // Target encounters an obstacle in front
                if (targetStopped)
                {
                    // Increases speed
                    UpdatePos(fastPosition);
                }
                // Target is advancing forward normally
                else
                {
                    // Normal speed
                    UpdatePos(normalPosition);

                    // Jumps if there's an obstacle in front and is grounded
                    if (Physics.CheckSphere(robberObstacleChecker.position, checkerRadious, obstacle)
                        && Physics.CheckSphere(robberObstacleChecker.position, checkerRadious, ground))
                    {
                        chaserRigidBody.velocity = new Vector3(chaserRigidBody.velocity.x,
                                                               jumpForce,
                                                               chaserRigidBody.velocity.z);
                    }
                }

                lastZposition = transform.localPosition.z;
            }
        }
    }

    /// <summary>
    /// Updates position as the X of player and a step in Z position
    /// </summary>
    void UpdatePos(float step)
    {
        transform.localPosition = new Vector3(player.localPosition.x,transform.localPosition.y,step);
    }

    void TargetStopped(object sender, bool stopped)
    {
        targetStopped = stopped;
    }
    
    void TargetCaught(object sender, EventArgs e)
    {
        targetCaught = true;
    }

    void ResetPosition(object sender, float resetDistance)
    {
        transform.localPosition = new Vector3(player.localPosition.x, 
                                              transform.localPosition.y, 
                                              player.localPosition.z - resetDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        robberObstacleChecker = GameObject.Find("Robber checker").transform;
        Gizmos.DrawSphere(robberObstacleChecker.position, checkerRadious);
    }
}