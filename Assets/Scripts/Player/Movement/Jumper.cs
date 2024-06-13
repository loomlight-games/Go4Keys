using System;
using UnityEngine;

/// <summary>
/// Provides jumping ability. Can detect when ground is hit.
/// </summary>
public class Jumper
{
    public event EventHandler JumpEvent;

    readonly Rigidbody rigidBody;
    readonly Transform groundChecker;
    readonly LayerMask groundLayer;
    readonly float jumpForce;

    public Jumper(Rigidbody rigidBody, Transform groundChecker, LayerMask groundLayer, float jumpForce)
    {
        this.rigidBody = rigidBody;
        this.groundChecker = groundChecker;
        this.groundLayer = groundLayer;
        this.jumpForce = jumpForce;
    }

    /// <summary>
    /// Moves rigidbody in Y axis. If jumpforce is positive it will jump
    /// </summary>
    public void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x,
                                         jumpForce,
                                         rigidBody.velocity.z);

        JumpEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Checks if ground checker collides with ground
    /// </summary>
    public bool IsGrounded()
    {
        // Ground is hit (ground checker collides with ground layer)
        if (Physics.CheckSphere(groundChecker.position, 0.2f, groundLayer))
            return true;

        return false;
    }
}
