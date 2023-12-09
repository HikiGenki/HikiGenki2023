using System.Collections;
using UnityEngine;

public class CameraFollowPlayerDirection : MonoBehaviour
{
    [SerializeField]
    private CameraControllerSettings settings;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float rotationSpeed = 5f;

    private Quaternion startTiltedRotation;
    private Quaternion targetRotation;

    private float cameraDist = 10f;

    private float tiltAngle;

    private void Awake()
    {
        startTiltedRotation = Quaternion.Euler(transform.localEulerAngles.x, 0f, 0f);
    }

    void Update()
    {
        //Set rotation
        targetRotation = PlayerForward() * startTiltedRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.localPosition = transform.rotation
            * new Vector3(0f, settings.yOffset, -cameraDist);
    }

    Quaternion PlayerForward()
    {
        var rot = player.eulerAngles;
        rot.x = 0f;
        return Quaternion.Euler(rot);
    }
}