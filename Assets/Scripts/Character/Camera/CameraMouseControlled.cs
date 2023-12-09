using UnityEngine;

public class CameraMouseControlled : MonoBehaviour
{
    [SerializeField]
    private Transform playerBody;

    [SerializeField]
    private CameraControllerSettings settings;

    private Quaternion targetRotation;
    private float cameraDist = 10f;

    /// <summary>
    /// The tilt that the camera has when following behind a moving character.
    /// </summary>
    private Quaternion followBehindTilt;
    private float interpolationSpeed;

    private void Awake()
    {
        followBehindTilt = Quaternion.Euler(settings.followBehindTiltAngle, 0f, 0f);
        targetRotation = followBehindTilt;
        interpolationSpeed = settings.freeLookInterpolationSpeed;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, interpolationSpeed * Time.deltaTime);

        transform.localPosition = transform.rotation * new Vector3(0f, settings.yOffset, -cameraDist);
    }

    public void RotateWithMouseMovement()
    {
        interpolationSpeed = settings.freeLookInterpolationSpeed;

        //Rotation
        Vector3 currentEuler = transform.rotation.eulerAngles;
        currentEuler.y += Input.GetAxis("Mouse X") * settings.panSpeed * Time.deltaTime;
        currentEuler.x -= Input.GetAxis("Mouse Y") * settings.freeLookTiltSpeed * Time.deltaTime;
        currentEuler.x = Mathf.Clamp(currentEuler.x, settings.freeLookMinTiltAngle, settings.freeLookMaxTiltAngle);

        targetRotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 0f);

        //Zoom
        cameraDist -= Input.mouseScrollDelta.y * settings.zoomSpeed * Time.deltaTime;
        cameraDist = Mathf.Clamp(cameraDist, settings.minZoomDistance, settings.maxZoomDistance);
    }

    public void FollowBehindObject(Transform target)
    {
        interpolationSpeed = settings.followBehindInterpolationSpeed;

        //Rotation of the character, multiplying the camera tilt, equals the final rotation
        targetRotation = GetUntiltedForwardQuaternion(target) * followBehindTilt;
    }

    #region Util

    public Vector3 GetUntiltedForwardVector(Transform target)
    {
        var untiltedForward = target.forward;
        untiltedForward.y = 0;
        return untiltedForward.normalized;
    }

    public Quaternion GetUntiltedForwardQuaternion()
    {
        return GetUntiltedForwardQuaternion(transform);
    }

    public Quaternion GetUntiltedForwardQuaternion(Transform target)
    {
        var rot = target.localEulerAngles;
        rot.x = 0f;
        return Quaternion.Euler(rot);
    }

    #endregion
}
