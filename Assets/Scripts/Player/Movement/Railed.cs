using UnityEngine;

/// <summary>
/// Provides lateral movement to the player, not to its parent.
/// </summary>
public class Railed
{
    readonly Transform player, rails;
    readonly float railChangeSpeed;
    float minX, maxX;
    float[] railsXPositions;
    bool isGyroscopeEnabled = false;

    public Railed(Transform player, Transform rails, float railChangeSpeed)
    {
        this.player = player;
        this.rails = rails;
        this.railChangeSpeed = railChangeSpeed;
    }

    public void Initialize()
    {
        // Creates array the size of number of children of railsParent
        railsXPositions = new float[rails.childCount];

        // Fills the array with the rails positions
        for (int i = 0; i < railsXPositions.Length; i++)
        {
            railsXPositions[i] = rails.GetChild(i).position.x;
        }

        minX = railsXPositions[0]; // Left rail
        maxX = railsXPositions[^1]; // Right rail [railsXPositions.Length - 1]

        if (UnityEngine.InputSystem.Gyroscope.current != null)
            isGyroscopeEnabled = UnityEngine.InputSystem.Gyroscope.current.enabled;
    }

    public void Update()
    {
        if (!isGyroscopeEnabled) return;

        // Use gyroscope input to move player
        float rotationY = InputManager.Instance.DeviceRotation().y;

        // Calculate new X position based on mobile rotation on Y-axis
        float newXPosition = player.localPosition.x + rotationY * railChangeSpeed * Time.deltaTime;

        // Clamp the new X position within the bounds of the rails
        newXPosition = Mathf.Clamp(newXPosition, minX, maxX);

        // Update player's position
        player.localPosition = new Vector3(newXPosition, player.localPosition.y, player.localPosition.z);
    }
}