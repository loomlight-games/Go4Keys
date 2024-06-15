using System;
using UnityEngine;

/// <summary>
/// Provides endless forward movement. Stops when detecting an obstacle in front.
/// Can detect an intersection.
/// </summary>
public class EndlessRunner
{
    public event EventHandler AtIntersectionEvent;
    public event EventHandler<bool> ObstacleInFront;

    readonly Transform runner;
    readonly float speed;
    readonly float checkerRadius = 0.4f;

    Transform obstacleChecker;
    LayerMask obstacle;

    public EndlessRunner(Transform transform, float forwardSpeed)//, Transform obstacleChecker, LayerMask obstacleLayer)
    {
        runner = transform;
        speed = forwardSpeed;
    }

    public void Initialize()
    {
        obstacleChecker = GameObject.Find("Player obstacle checker").transform;
        obstacle = 1 << LayerMask.NameToLayer("Obstacle");
    }

    public void Update()
    {
        // Obstacle in front (obstacle checker collides with obstacle layer)
        if (Physics.CheckSphere(obstacleChecker.position, checkerRadius, obstacle))
        {
            ObstacleInFront?.Invoke(this, true);
        }
        else
        {
            ObstacleInFront?.Invoke(this, false);
            runner.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Intersection"))
            AtIntersectionEvent?.Invoke(this, EventArgs.Empty);
    }
}