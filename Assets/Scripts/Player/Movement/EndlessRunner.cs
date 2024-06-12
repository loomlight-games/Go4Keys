using UnityEngine;

/// <summary>
/// Provides endless forward movement. Stops when detecting an obstacle in front.
/// </summary>
public class EndlessRunner
{
    readonly Transform transform;
    readonly float forwardSpeed;
    readonly Transform obstacleChecker;
    readonly LayerMask obstacleLayer;
    float forwardValue;

    public EndlessRunner(Transform transform, float forwardSpeed, Transform obstacleChecker, LayerMask obstacleLayer)
    {
        this.transform = transform;
        this.forwardSpeed = forwardSpeed;
        this.obstacleChecker = obstacleChecker;
        this.obstacleLayer = obstacleLayer;
    }

    public void Update()
    {
        // Obstacle is hit (obstacle checker collides with obstacle layer)
        if (Physics.CheckSphere(obstacleChecker.position, .4f, obstacleLayer))
            forwardValue = 0.0f; // Stops moving
        else // Not hitting any obstacle
            forwardValue = forwardSpeed; // Resumes moving

        //Move the object at forward speed to the orientation it's facing
        transform.Translate(forwardValue * Time.deltaTime * Vector3.forward);
    }
}