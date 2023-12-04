using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{


    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float gravityMaxSpeed = -20f;
    public float gravityAcceleration = 10f;

    private Rigidbody playerRigidbody;

    //Movement
    private Vector3 velocity = Vector3.zero;


    //Camera 
    private Transform cameraTransform;
    private Vector3 nonTiltedCameraDirection;
    private Quaternion nonTiltedCameraRotation;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        DirectionalInputUpdate();
        JumpInputUpdate();
        ApplyGravity();

        UpdateCameraForward();

        playerRigidbody.velocity = nonTiltedCameraRotation * velocity;
    }

    private void UpdateCameraForward()
    {
        nonTiltedCameraDirection = cameraTransform.forward;
        nonTiltedCameraDirection.y = 0f;

        nonTiltedCameraRotation = Quaternion.LookRotation(nonTiltedCameraDirection, Vector3.up);
    }

    private void DirectionalInputUpdate()
    {
        //LEFT - RIGHT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            velocity.x = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            velocity.x = 1f;
        }
        else
        {
            velocity.x = 0f;
        }

        //UP - DOWN
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            velocity.z = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            velocity.z = -1f;
        }
        else
        {
            velocity.z = 0f;
        }

        velocity.x *= moveSpeed;
        velocity.z *= moveSpeed;
    }

    private void JumpInputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpSpeed;
            //TODO: Coyote timer, Jump queue Timer
        }
    }

    private void ApplyGravity()
    {
        velocity.y = (velocity.y > gravityMaxSpeed) ?
            velocity.y - Time.deltaTime * gravityAcceleration :
            gravityMaxSpeed;
    }
}
