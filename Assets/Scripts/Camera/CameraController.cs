using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float height = 5f;
    public float distance = 10f;
    public float angle = 60f;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 targetPosition = player.position - player.forward * distance + Vector3.up * height;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Rotate the camera to face the player at the specified angle
        transform.rotation = Quaternion.Euler(new Vector3(angle, player.eulerAngles.y, 0f));
    }

    void OnGUI()
    {
        // Center the player in the middle of the screen
        float playerScreenPosX = Camera.main.WorldToScreenPoint(player.position).x;
        float centerX = Screen.width / 2;
        float offsetX = playerScreenPosX - centerX;
        GUIUtility.RotateAroundPivot(-angle, new Vector2(centerX, Screen.height / 2));
        GUI.matrix = Matrix4x4.TRS(new Vector3(offsetX, 0, 0), Quaternion.identity, Vector3.one);
    }
}