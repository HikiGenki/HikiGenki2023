using UnityEngine;

public class Player_Controller_3D_RelativeToCamera : MonoBehaviour
{
    // Customizable settings, should always be public for easy access outside the script 
    [Header("Movement Settings")]
    [Tooltip("Default speed is 4")]
    public float PLAYER_SPEED;

    [Tooltip("Default Jump Height is 5")]
    public float PLAYER_JUMP_HEIGHT;

    [Header("Abilities Settings")]
    public bool PLAYER_ABILITY_DOUBLEJUMP;

    public CameraMouseControlled CAMERA_CONTROLLER;

    //Requirements for Player to work, script will detect them automatically 
    private Rigidbody PLAYER_RIGIDBODY;
    private Animator PLAYER_ANIMATOR;
    private Transform PLAYER_BODY;
    private Transform PLAYER_CAMERA;

    //General variables
    private bool PLAYER_CAN_JUMP;
    private int PLAYER_JUMPED_TIMES;
    private int PLAYER_JUMP_LIMIT;
    private Vector3 PLAYER_FACING;

    private Quaternion CAMERA_ROTATION;

    private const string LOOK_BUTTON_KEY = "Look";

    private void Start()
    {
        //detecting if player has requirements TODO:Prevent from launching if one of these doesn't exist
        PLAYER_RIGIDBODY = GetComponent<Rigidbody>();
        PLAYER_ANIMATOR = GetComponent<Animator>();
        PLAYER_BODY = transform.GetChild(0);
        PLAYER_CAMERA = transform.GetComponentInChildren<Camera>().transform;

        //Basic settings
        PLAYER_CAN_JUMP = true;
        PLAYER_JUMP_HEIGHT = 5;
        PLAYER_SPEED = 4;
        PLAYER_FACING = PLAYER_BODY.forward;

        PLAYER_JUMP_LIMIT = PLAYER_ABILITY_DOUBLEJUMP ? 2 : 1;
    }

    private void Update()
    {
        CacheCameraRotation();

        if (Input.GetButton(LOOK_BUTTON_KEY))
        {
            CAMERA_CONTROLLER.RotateWithMouseMovement();
        }
        else
        {
            if (IsDirectionKeyPressed())
            {
                CAMERA_CONTROLLER.FollowBehindObject(PLAYER_BODY);
            }
        }

        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");    //Arrows and WSAD
        float VerticalInput = Input.GetAxis("Vertical");        //I think we should get rid of arrow input later on

        PLAYER_RIGIDBODY.velocity = CAMERA_ROTATION * new Vector3(
            HorizontalInput * PLAYER_SPEED,
            PLAYER_RIGIDBODY.velocity.y,
            VerticalInput * PLAYER_SPEED);

        if ((HorizontalInput != 0) || (VerticalInput != 0))
        {
            PLAYER_ANIMATOR.SetBool("CanWalk", true);
        }
        else
        {
            PLAYER_ANIMATOR.SetBool("CanWalk", false);
        }

        if (PLAYER_CAN_JUMP)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f))
            {
                PLAYER_JUMPED_TIMES = 0;
            }

            Debug.DrawRay(transform.position, -Vector3.up * 0.1f, Color.red);

            if ((Input.GetKeyDown(KeyCode.Space)) && (PLAYER_JUMPED_TIMES < PLAYER_JUMP_LIMIT))
            {
                PLAYER_JUMPED_TIMES++;
                PLAYER_RIGIDBODY.AddForce(transform.up * PLAYER_JUMP_HEIGHT, ForceMode.Impulse);
            }
        }

        //Player rotation
        if (IsDirectionKeyPressed())
        {
            PLAYER_BODY.rotation = CAMERA_ROTATION *
                 Quaternion.LookRotation(new Vector3(HorizontalInput, 0, VerticalInput).normalized);
        }
    }

    private void CacheCameraRotation()
    {
        CAMERA_ROTATION = CAMERA_CONTROLLER.GetUntiltedForwardQuaternion();
    }

    private bool IsDirectionKeyPressed()
    {
        return (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f ||
            Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f);
    }
}