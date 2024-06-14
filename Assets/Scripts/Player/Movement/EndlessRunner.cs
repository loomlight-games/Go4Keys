using System;
using UnityEngine;

/// <summary>
/// Provides endless forward movement. Stops when detecting an obstacle in front.
/// Can detect an intersection.
/// </summary>
public class EndlessRunner
{
    public event EventHandler AtIntersectionEvent;

    readonly Transform runner;
    readonly float forwardSpeed;
    readonly float checkerRadius = 0.4f;

    Transform obstacleChecker;
    LayerMask obstacle;

    public EndlessRunner(Transform transform, float forwardSpeed)//, Transform obstacleChecker, LayerMask obstacleLayer)
    {
        this.runner = transform;
        this.forwardSpeed = forwardSpeed;
    }

    public void Initialize()
    {
        obstacleChecker = GameObject.Find("Player obstacle checker").transform;
        obstacle = 1 << LayerMask.NameToLayer("Obstacle");
    }

    public void Update()
    {
        // Obstacle is NOT in front (obstacle checker collides with obstacle layer)
        if (!Physics.CheckSphere(obstacleChecker.position, checkerRadius, obstacle))
        {
            runner.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Intersection"))
            AtIntersectionEvent?.Invoke(this, EventArgs.Empty);
    }
}