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

    public float speed = 6.0f;
    public static float jumpSpeed;
    public float gravity;
    public bool inAir = false;
    public float airTime;
    public PlayerFollow cameraControl;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 verticalResult = Vector3.zero;
    private Vector3 horizontalResult = Vector3.zero;
    private Vector3 jumpVector = Vector3.zero;
    private Vector3 detectGround = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;
    public int jumpCount;
    public static int flyStamina;
    public static int charJumps;
    public static int charJumpHeight;
    public static float fallMultiplier = 50f;
    private float velocity;
    private float debugValue;
    public bool isJump;
    public GameObject floorRayObj;

    void Start()
    {
        airTime = 0;
        characterController = GetComponent<CharacterController>();
        jumpCount = 0;
        jumpSpeed = 50;
        gravity = 1;
    }
    
    void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime); //
        AirControl();
        //Debug.Log(LayerMask.GetMask("Lava"));
        //moveDirection.y -= (gravity * Time.deltaTime * Time.deltaTime);
        Jump();
        CalcMove();

        Debug.DrawRay(gameObject.transform.position, jumpVector, Color.red);
        Debug.DrawRay(gameObject.transform.position, moveDirection, Color.magenta);
        detectGroundFunc();
        
        FallControl();
    }

    void AirControl() //this function should adjust player speed depending on if they're in the air or not
    {
        if (inAir == false)
        {
            
            jumpCount = charJumps;
            speed = 6.0f;
        }
        if (inAir == true)
        {
            //Debug.Log("I'm in the air!");
            speed = 3.0f;
        }
    }
    void detectGroundFunc()//this is supposed to detect whether the player is on the ground, and set the bool inAir to true, to adjust calculations of air flying/double jumps, etc
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, LayerMask.GetMask("Floor")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Floor");
            inAir = false;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, LayerMask.GetMask("Slime")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Slime!");
            inAir = false;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, LayerMask.GetMask("Lava")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("AHH I'M ON FIRE!!!");
            inAir = false;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .5f, LayerMask.GetMask("Liquid")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("I'm touching Liquid!!");
            inAir = false;
        }
        else
        {
            Debug.Log("I'M FALLING!");
            inAir = true;
        }


        /*if ((characterController.collisionFlags & CollisionFlags.Below) == 0)  <== this don't work because we have gravity coded to being in air
         * 
        {
            airTime++;
            if (airTime >= 100)
            {
                inAir = true;
            }
        }
        else
        {
            print("Touching ground!");
            inAir = false;
            airTime = 0;
        }
        
        if (characterController.isGrounded == false)
        {
            inAir = true;
        }
        else
        {
            inAir = false;
        }
        */
    }
    void Jump() //this functions adjusts the jump vector, which gets added to the other movement vectors later on
        //this jump vector is affected by each character's respective jump speed modifier, and is multiplied to get better results with easier to keep track of numbers
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            debugValue = jumpVector.y;
            while(jumpVector.y <= charJumpHeight)
            {
                jumpVector.y += jumpSpeed * Time.deltaTime;
            }
            //moveDirection.y += jumpSpeed * 5;
            //characterController.Move(jumpVector * jumpSpeed);
            Debug.Log("i've jumped!");
            jumpCount--;
            isJump = true;
        }
        else 
        {
            jumpVector.y = 0;
            isJump = false;
        }
    }
    void FallControl() //while velocity is below 0, increase gravity by fall multiplier over time
    {
        if(inAir == true)
        {
            //moveDirection.y -= gravity * (fallMultiplier - 1) * Time.deltaTime;
            moveDirection.y -= gravity * Time.deltaTime * 10f;
            Debug.Log("gravity is on!");

            if (velocity < -.2 && inAir == true && jumpCount == 0)//this line increases gravity by fall mult over time while the player is falling, capping off at 100
            {
                gravity += fallMultiplier * Time.deltaTime; 
                    if (gravity >= 500)
                    {
                        gravity = 500;
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
            gravity = 150; //this line resets gravity back to its default when it touches the ground

        }
    }
    void CalcMove()
    {
        velocity = Mathf.Round(characterController.velocity.y);
        horizontalInput = Input.GetAxis("Horizontal"); //take the input of the player, either -1 or 1, and put that into the camera
        verticalInput = Input.GetAxis("Vertical");

        verticalResult = cameraControl.camForward * verticalInput; //we take cam control, which is shooting a vector forward, multiply it by input to get true camera forward
        horizontalResult = cameraControl.camRight * horizontalInput; //same as above, but with the right vector, and horizontal input
        /*
         * could have used some lerp or something, but i'm struggling to learn how they actually work, and what i've done seems to work just fine..? maybe..?
        */
        moveDirection = ((verticalResult + horizontalResult) * speed) + jumpVector;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        
    }
    void FixedUpdate() //
    {
        
    }

}