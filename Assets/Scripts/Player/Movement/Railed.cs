using UnityEngine;

/// <summary>
/// Provides lateral movement between rails.
/// </summary>
public class Railed
{
    readonly Transform player;
    readonly float railChangeSpeed;

    float[] railsXPositions;
    public int currentRailIndex = 1;
    Vector3 currentRailPos;

    public Railed(Transform player, float railChangeSpeed)
    {
        this.player = player;
        this.railChangeSpeed = railChangeSpeed;
    }

    public void Initialize()
    {
        Transform railsParent = GameObject.Find("Rails").transform;

        // Creates array the size of number of children of railsParent
        railsXPositions = new float[railsParent.childCount];

        // Fills the array with the rails positions
        for (int i = 0; i < railsXPositions.Length; i++)
        {
            railsXPositions[i] = railsParent.GetChild(i).position.x;
        }
    }

    public void Update()
    {
        // Check if the gyroscope is available
        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            // Use gyroscope input to move player
            float rotationX = InputManager.Instance.DeviceRotation().x;
            float rotationY = InputManager.Instance.DeviceRotation().y;
            float rotationZ = InputManager.Instance.DeviceRotation().z;

            // Display gyroscope data using debugText
            GameManager.Instance.debugText.text = $"Gyroscope rotation - X: {rotationX}, Y: {rotationY}, Z: {rotationZ}";

            // Calculate new X position based on rotation
            float newXPosition = player.localPosition.x + rotationX * railChangeSpeed * Time.deltaTime;

            // Clamp the new X position within the bounds of the rails
            float minX = railsXPositions[0]; // Left rail
            float maxX = railsXPositions[railsXPositions.Length - 1]; // Right rail
            newXPosition = Mathf.Clamp(newXPosition, minX, maxX);

            // Update player's position
            player.localPosition = new Vector3(newXPosition, player.localPosition.y, player.localPosition.z);
        }
        else
        {
            GameManager.Instance.debugText.text = "No gyroscope available.";
        }
    }
}