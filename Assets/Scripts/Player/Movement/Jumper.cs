using System;
using UnityEngine;

/// <summary>
/// Provides jumping ability. Can detect when ground is hit.
/// </summary>
public class Jumper
{
    public event EventHandler JumpEvent;
    public event EventHandler GroundedEvent;

    readonly Rigidbody rigidBody;    
    readonly float jumpForce;
    readonly float checkerRadius = 0.2f;

    Transform groundChecker;
    LayerMask ground;

    public Jumper(Rigidbody rigidBody, float jumpForce)
    {
        this.rigidBody = rigidBody;
        this.jumpForce = jumpForce;
    }

    public void Initialize()
    {
        groundChecker = GameObject.Find("Player ground checker").transform;
        ground = 1 << LayerMask.NameToLayer("Ground");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Moves rigidbody in Y axis. If jumpforce is positive it will jump
    /// </summary>
    public void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
    }

    /// <summary>
    /// Checks if ground checker collides with ground
    /// </summary>
    public void IsGrounded()
    {
        // Ground is hit (ground checker collides with ground layer)
        if (Physics.CheckSphere(groundChecker.position, checkerRadius, ground))
            GroundedEvent?.Invoke(this, EventArgs.Empty);
    }
}
