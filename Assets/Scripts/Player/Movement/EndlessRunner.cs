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

    public EndlessRunner(Transform transform, float forwardSpeed, Transform obstacleChecker, LayerMask obstacleLayer)
    {
        this.transform = transform;
        this.forwardSpeed = forwardSpeed;
        this.obstacleChecker = obstacleChecker;
        this.obstacleLayer = obstacleLayer;
    }

    public void Update()
    {
        // Obstacle is NOT in front (obstacle checker collides with obstacle layer)
        if (!Physics.CheckSphere(obstacleChecker.position, .4f, obstacleLayer))
            transform.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);
    }
}