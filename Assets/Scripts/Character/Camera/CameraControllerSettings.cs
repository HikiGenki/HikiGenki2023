using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSettings", menuName = "Scriptable Object/CameraControllerSettings", order = 0)]
public class CameraControllerSettings : ScriptableObject
{
    [Header("Free Look")]
    public float panSpeed = 180f;

    [Space]
    public float freeLookTiltSpeed = 60f;
    public float freeLookMinTiltAngle = 2f;
    public float freeLookMaxTiltAngle = 75f;

    [Space]
    public float freeLookInterpolationSpeed = 20f;

    [Header("Zoom")]
    public float zoomSpeed = 50f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 15f;

    [Header("Follow behind")]
    public float followBehindInterpolationSpeed = 5f;
    public float followBehindTiltAngle = 20f;

    [Header("Camera position")]
    [Tooltip("How high above the root position (0, 0, 0) should the camera aim at.")]
    public float yOffset = 1f;
}