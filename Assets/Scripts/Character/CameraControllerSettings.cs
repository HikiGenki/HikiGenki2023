using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSettings", menuName = "Scriptable Object/CameraControllerSettings", order = 0)]
public class CameraControllerSettings : ScriptableObject
{
    [Header("Camera control")]
    public float panSpeed = 180f;

    [Space]
    public float tiltSpeed = 60f;

    public float minTiltAngle = 2f;

    public float maxTiltAngle = 75f;

    [Space]
    public float zoomSpeed = 50f;

    public float minZoomDistance = 2f;

    public float maxZoomDistance = 15f;

    [Header("Camera position")]
    [Tooltip("How high above the root position (0, 0, 0) should the camera aim at.")]
    public float yOffset = 1f;
}