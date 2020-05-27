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
    
    public PlayerFollow cameraControl;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 verticalResult = Vector3.zero;
    private Vector3 horizontalResult = Vector3.zero;
    private Vector3 jumpVector = Vector3.up;
    private float horizontalInput;
    private float verticalInput;
    public int jumpCount;
    public static int flyStamina;
    public static int charJumpMod;
    public static float fallMultiplier = 2f;
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
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpSpeed * 5;
            //characterController.Move(jumpVector * jumpSpeed);
        }
    }
    void FallControl() //while velocity is below 0, increase gravity by fall multiplier over time
    {
        if(characterController.isGrounded == false)
        {
            moveDirection.y -= gravity * (fallMultiplier - 1) * Time.deltaTime; 

            if (velocity < -.1 )
            {
                gravity += fallMultiplier * Time.deltaTime; //this line increases gravity by fall mult over time while the player is falling, capping off at 100
                    if (gravity >= 100)
                    {
                        gravity = 100;
                    }
            } 
                    else 
                    {
                        gravity = 1; //this line resets gravity back to its default when it touches the ground
                    }

            if (Input.GetButton("Crouch"))
            {
                gravity += fallMultiplier * 2 * Time.deltaTime; //this should double gravity while the crouch button is pressed, allowing faster falling if desired
                return;
            }
    
        }
    }
    void CalcMove()
    {
        velocity = characterController.velocity.y;
        horizontalInput = Input.GetAxis("Horizontal"); //take the input of the player, either -1 or 1, and put that into the camera
        verticalInput = Input.GetAxis("Vertical");

        verticalResult = cameraControl.camForward * verticalInput; //we take cam control, which is shooting a vector forward, multiply it by input to get true camera forward
        horizontalResult = cameraControl.camRight * horizontalInput; //same as above, but with the right vector, and horizontal input
        /*
         * i was going to write someting here but forgot
        */
        moveDirection = verticalResult + horizontalResult;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime * 2f;
    }
    void FixedUpdate() //
    {
        
    }

}