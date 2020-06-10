using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class Movement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 3f;
    public static float jumpSpeed;
    [SerializeField] public static float gravity;
    public bool inAir = false;
    public float airTime;
    public PlayerCamera cameraControl;
    public Vector3 moveDirection = Vector3.zero;
    private Vector3 verticalResult = Vector3.zero;
    private Vector3 horizontalResult = Vector3.zero;
    private Vector3 jumpVector = Vector3.zero;
    private Vector3 detectGround = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;
    public int jumpCount;
    public static float flyStamina;
    public static int charJumps;
    public static int charJumpHeight;
    public static float fallMultiplier = 50f;
    private float velocity;
    public float debugValue;
    public bool isJump;
    private bool canFly;
    public GameObject floorRayObj;
    private int floorMask;
    private int lavaMask;
    private int liquidMask;
    private int slimeMask;
    private int currentRay;
    public illyaGlide illyaGlide;
    public CharacterState characterState;
    private float jumpCheck;
    private int i;

    void Start()
    {
        characterState.CheckCharacter();
        illyaGlide = gameObject.GetComponent<illyaGlide>();
        airTime = 0;
        characterController = GetComponent<CharacterController>();
        floorMask = LayerMask.GetMask("Floor");
        lavaMask = LayerMask.GetMask("Lava");
        liquidMask = LayerMask.GetMask("Liquid");
        slimeMask = LayerMask.GetMask("Slime");
        gravity = characterState.characterGravity;
    }

    void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime); //
        AirControl();
        //Debug.Log(LayerMask.GetMask("Lava"));
        //moveDirection.y -= (gravity * Time.deltaTime * Time.deltaTime);
        Jump();
        
        //Debug.Log(gravity);
        Debug.DrawRay(gameObject.transform.position, jumpVector, Color.red);
        Debug.DrawRay(gameObject.transform.position, moveDirection, Color.magenta);
        detectGroundFunc();
       
        CalcMove();
        FlyCheck();
        Fly();
        if (isJump == true)
        {
            
            JumpControl();
            //Debug.Log("i'm jumping");
            
        }
        else 
        { 
            FallControl();
            //Debug.Log("i'm falling");
            jumpVector.y = 0;
        }
    }

    void AirControl() //this function should adjust player speed depending on if they're in the air or not
    {
        if (inAir == false)
        {

            jumpCount = charJumps; //reset the character's jumps when hitting ground
        }
        if (inAir == true)
        {
            //Debug.Log("I'm in the air!");
            speed = 2.0f;
        }
    }
    void detectGroundFunc()//this is supposed to detect whether the player is on the ground, and set the bool inAir to true, to adjust calculations of air flying/double jumps, etc
    {
        int floorMask = LayerMask.GetMask("Floor");
        int slimeMask = LayerMask.GetMask("Slime");
        int lavaMask = LayerMask.GetMask("Lava");
        int liquidMask = LayerMask.GetMask("Liquid");
        int playerMask = LayerMask.GetMask("Player");

        floorMask -= playerMask;
        slimeMask -= playerMask;
        lavaMask -= playerMask;
        liquidMask -= playerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, floorMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Floor");
            inAir = false;
            TouchFloor();
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, slimeMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Slime!");
            inAir = false;
            TouchSlime();
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, lavaMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("AHH I'M ON FIRE!!!");
            inAir = false;
            TouchLava();
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, liquidMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Liquid!!");
            inAir = false;
            TouchLiquid();
        }
        else
        {
            Debug.Log("I'M FALLING!");
            inAir = true;
        }
    }
    void JumpControl() //code from illya, this takes the movedirection.y and changes it to the jump speed, which somehow magically does eerything i want it to
    {
        moveDirection.y = jumpSpeed;
        jumpCheck += moveDirection.y;
        if (jumpCheck > charJumpHeight)
        {
            isJump = false;
            jumpCheck = 0;
        }

    }
    void Jump() //this functions adjusts the jump vector, which gets added to the other movement vectors later on
                //this jump vector is affected by each character's respective jump speed modifier, and is multiplied to get better results with easier to keep track of numbers
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
           isJump =true;
           jumpCount -= 1;
            
            
            
            
            /*jumpCount -= 1;
            debugValue = jumpVector.y;
            while (jumpVector.y <= charJumpHeight) //until you hit the character's jump height, accelerate upwards
            {
                jumpVector.y += jumpSpeed * Time.deltaTime;
            }
            //moveDirection.y += jumpSpeed * 5;
            //characterController.Move(jumpVector * jumpSpeed);
            //Debug.Log("i've jumped!");
            isJump = true;
        }
        else
        {
            jumpVector.y = 0;
            isJump = false;
            */
        }

    }
    void FallControl() //while velocity is below 0, increase gravity by fall multiplier over time
    {
        if (inAir == true)
        {
            //moveDirection.y -= gravity * (fallMultiplier - 1) * Time.deltaTime;
            moveDirection.y -= gravity * Time.deltaTime;
            //Debug.Log("gravity is on!");
            float prevGrav = gravity;
            if (velocity < -.2 && inAir == true && jumpCount == 0)//this line increases gravity by fall mult over time while the player is falling, capping off at 100
            {
                gravity += fallMultiplier * Time.deltaTime;
                if (gravity >= prevGrav)
                {
                    gravity = prevGrav;
                }
            }
            if (Input.GetButton("Crouch")) //this should double gravity while the crouch button is pressed, allowing faster falling if desired
            {
                gravity += fallMultiplier * 2 * Time.deltaTime;
                return;
            }

        }
        else
        {
            gravity = characterState.characterGravity; //this line resets gravity back to its default when it touches the ground

        }
    }
    void CalcMove()
    {
        velocity = Mathf.Round(characterController.velocity.y);
        horizontalInput = Input.GetAxis("Horizontal"); //take the input of the player, either -1 or 1, and put that into the camera
        verticalInput = Input.GetAxis("Vertical");

        
        verticalResult.x = cameraControl.camForward.x * verticalInput; //we take cam control, which is shooting a vector forward, multiply it by input to get true camera forward
        verticalResult.z = cameraControl.camForward.z * verticalInput;
        
        horizontalResult.x = cameraControl.camRight.x * horizontalInput; //same as above, but with the right vector, and horizontal input
        horizontalResult.z = cameraControl.camRight.z * horizontalInput;
        /*
         * could have used some lerp or something, but i'm struggling to learn how they actually work, and what i've done seems to work just fine..? maybe..?
        */
        moveDirection.x = ((verticalResult.x + horizontalResult.x) * speed); //+ (jumpVector);
        moveDirection.z = ((verticalResult.z + horizontalResult.z) * speed);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)

    }
    void TouchFloor() //this function changes the player speed by a set value based on materials in detectGroundFun(), then multiplies it by the character speed mod to get a better speed
    {
        speed = 4f * characterState.speedMod;
    }
    void TouchLava()
    {
        speed = 8f * characterState.speedMod;
    }
    void TouchLiquid()
    {
        speed = 2f * characterState.speedMod;
    }
    void TouchSlime()
    {
        speed = 1f * characterState.speedMod;
    }
    void FlyCheck() //checks if you are in air, and if stamina is above 0
    {
        if (inAir == true && flyStamina > 0)
        {
            canFly = true;
        }
        else
        {
            canFly = false;
        }
    }
    void Fly()
    {
        if (canFly == true)
        {
            if (Input.GetButton("Jump") && flyStamina >= 1 && isJump == false)
            {
                gravity = 0;
                moveDirection.y = 10;
                //Debug.Log("i'm flying!");
                flyStamina--;
                illyaGlide.startGlide = false;
                
                if (flyStamina <= 1)
                {
                    illyaGlide.startGlide = true;
                }
            }
        }
        else if (canFly == false && flyStamina <= 200)
        {
            flyStamina += characterState.regenMod * Time.deltaTime;
            if (flyStamina == 200)
            {
                flyStamina = 200;
            }
        }
    }
}