using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CameraControllerSettings settings;

    private const string LookButtonName = "Look";

    private float cameraDist = 10f;

    private void Awake()
    {
    }

    private void Start()
    {

    }

    private void Update()
    {
        //Rotation
        if (Input.GetButton(LookButtonName))
        {
            Vector3 currentEuler = transform.rotation.eulerAngles;
            currentEuler.y += Input.GetAxis("Mouse X") * settings.panSpeed * Time.deltaTime;
            currentEuler.x -= Input.GetAxis("Mouse Y") * settings.tiltSpeed * Time.deltaTime;
            currentEuler.x = Mathf.Clamp(currentEuler.x, settings.minTiltAngle, settings.maxTiltAngle);

            transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 0f);
        }

        //Zoom
        cameraDist -= Input.mouseScrollDelta.y * settings.zoomSpeed * Time.deltaTime;
        cameraDist = Mathf.Clamp(cameraDist, settings.minZoomDistance, settings.maxZoomDistance);

        //Set position
        transform.localPosition = transform.rotation * new Vector3(0f, settings.yOffset, -cameraDist);
        //transform.rotation = transform.localRotation * Quaternion.Euler(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);


        //Input.mouseScrollDelta.y
    }

    public Vector3 GetUntiltedForwardDirection()
    {
        var untiltedForward = transform.forward;
        untiltedForward.y = 0;
        return untiltedForward;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "mouse wheel " + Input.GetAxis("Mouse ScrollWheel"));
    }
}
