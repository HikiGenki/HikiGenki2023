//using UnityEngine;
//public class Player_Controller_3D : MonoBehaviour
//{
//    // Customizable settings, should always be public for easy access outside the script 
//    [Header("Movement Settings")]
//    [Tooltip("Default speed is 4")]
//    public float PLAYER_SPEED;
//    [Tooltip("Default Jump Height is 5")]
//    public float PLAYER_JUMP_HEIGHT;


//    [Header("Abilities Settings")]
//    public bool PLAYER_ABILITY_DOUBLEJUMP;
//    //Requirements for Player to work, script will detect them automatically 
//    private Rigidbody PLAYER_RIGIDBODY;
//    private Animator PLAYER_ANIMATOR;
//    private Transform PLAYER_BODY;
//    private Camera PLAYER_CAMERA;
//    //General variables
//    private bool PLAYER_CAN_JUMP;
//    private int PLAYER_JUMPED_TIMES;
//    private int PLAYER_JUMP_LIMIT;



//    private void Start()
//    {
//        //detecting if player has requirements TODO:Prevent from launching if one of these doesn't exist
//        PLAYER_RIGIDBODY = GetComponent<Rigidbody>();
//        PLAYER_ANIMATOR = GetComponent<Animator>();
//        PLAYER_BODY = transform.GetChild(0);
//        PLAYER_CAMERA = transform.GetChild(1).GetComponent<Camera>();
//        //Basic settings
//        PLAYER_CAN_JUMP = true;
//        PLAYER_JUMP_HEIGHT = 5;
//        PLAYER_SPEED = 4;

//    }
//    private void Update()
//    {
//        PlayerMovement();
//        //PlayerAbilities();



//    }
//    private void PlayerMovement()
//    {
//        float HorizontalInput = Input.GetAxis("Horizontal");    //Arrows and WSAD
//        float VerticalInput = Input.GetAxis("Vertical");        //I think we should get rid of arrow input later on
//        PLAYER_RIGIDBODY.velocity = new Vector3(HorizontalInput * PLAYER_SPEED, PLAYER_RIGIDBODY.velocity.y, VerticalInput * PLAYER_SPEED);

//        if ((HorizontalInput != 0) || (VerticalInput != 0))
//        {
//            PLAYER_ANIMATOR.SetBool("CanWalk", true);
//        }
//        else
//        {
//            PLAYER_ANIMATOR.SetBool("CanWalk", false);
//        }

//        if (PLAYER_CAN_JUMP)
//        {
//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f))
//            {
//                PLAYER_JUMPED_TIMES = 0;
//                Debug.Log(PLAYER_JUMP_LIMIT);
//            }
//            Debug.DrawRay(transform.position, -Vector3.up * 0.1f, Color.red);
//            if ((Input.GetKeyDown(KeyCode.Space)) && (PLAYER_JUMPED_TIMES < PLAYER_JUMP_LIMIT))
//            {
//                PLAYER_JUMPED_TIMES++;
//                PLAYER_RIGIDBODY.AddForce(transform.up * PLAYER_JUMP_HEIGHT, ForceMode.Impulse);
//            }

//        }
//        //Player rotation (There should be a better way to code this, I know, I'll change it later xD)
//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            PLAYER_BODY.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
//        }
//        else if (Input.GetKeyDown(KeyCode.A))
//        {
//            PLAYER_BODY.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
//        }
//        else if (Input.GetKeyDown(KeyCode.S))
//        {
//            PLAYER_BODY.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
//        }
//        else if (Input.GetKeyDown(KeyCode.D))
//        {
//            PLAYER_BODY.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
//        }


//    }


//    private void PlayerAbilities()
//    {
//        if (PLAYER_ABILITY_DOUBLEJUMP)
//        {
//            PLAYER_JUMP_LIMIT = 2;
//        }
//    }

//}