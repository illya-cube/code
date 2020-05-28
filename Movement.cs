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
    public PlayerFollow cameraControl;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 verticalResult = Vector3.zero;
    private Vector3 horizontalResult = Vector3.zero;
    private Vector3 jumpVector = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;
    public int jumpCount;
    public static int flyStamina;
    public static int charJumpMod;
    public static float fallMultiplier = 50f;
    private float velocity;
    private float debugTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpCount = 0;
        jumpSpeed = 50;
        gravity = 1;
    }
    
    void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime * speed);
        AirControl();
        FallControl();
        //moveDirection.y -= (gravity * Time.deltaTime * Time.deltaTime);
        Jump();
        CalcMove();

        Debug.DrawRay(gameObject.transform.position, jumpVector, Color.red);
        Debug.DrawRay(gameObject.transform.position, moveDirection, Color.magenta);
        if(characterController.isGrounded == false)
        {
            inAir = true;
        }
        else
        {
            inAir = false;
        }
    }

    void AirControl() //this function should adjust player speed depending on if they're in the air or not
    {
        if (characterController.isGrounded)
        {
            
            jumpCount = 1 + charJumpMod;
            speed = 6.0f;
        }
        if (characterController.isGrounded == false)
        {
            //Debug.Log("I'm in the air!");
            speed = 3.0f;
        }
    }
    void Jump() //this 
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpVector.y += jumpSpeed * 2;
            //moveDirection.y += jumpSpeed * 5;
            //characterController.Move(jumpVector * jumpSpeed);
            Debug.Log("i've jumped!");
            jumpCount--;
        }
        else 
        {
            jumpVector.y = 0;
        }
    }
    void FallControl() //while velocity is below 0, increase gravity by fall multiplier over time
    {
        if(inAir == true)
        {
            //moveDirection.y -= gravity * (fallMultiplier - 1) * Time.deltaTime;
            moveDirection.y -= gravity * Time.deltaTime * 10f;
            Debug.Log("gravity is on!");

            if (velocity < -.2 && inAir == true)//this line increases gravity by fall mult over time while the player is falling, capping off at 100
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
        moveDirection = verticalResult + horizontalResult + jumpVector;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime * 2;
    }
    void FixedUpdate() //
    {
        
    }

}