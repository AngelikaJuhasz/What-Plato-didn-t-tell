using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // The player to follow
    public float smoothSpeed = 0.125f; // How smooth the camera movement is
    public Vector3 offset;          // Offset to maintain a distance from the player

    // The boundaries of the camera (left, right, top, bottom)
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        // If there's no player, do nothing
        if (player == null)
            return;

        // Get the desired position (player's position + offset)
        Vector3 desiredPosition = player.position + offset;

        // Clamp the desired position to stay within the boundaries
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Create the final camera position
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Smoothly move the camera to the final position
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }

    // This will help visualize the camera bounds in the Scene view
    private void OnDrawGizmos()
    {
        // Draw the camera boundaries in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(minX, maxY, 0));  // Left line
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(maxX, maxY, 0));  // Top line
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(maxX, minY, 0));  // Right line
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(minX, minY, 0));  // Bottom line
    }
}
