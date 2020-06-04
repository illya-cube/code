using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform LookTarget;
    [SerializeField] private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public bool RotateMiddleMouseButton = false;
    public bool lockedOn = false;
    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;
    public float CameraPitchMax = 6.5f;
    public GameObject lookCamera;
    public GameObject lockTarget;
    public GameObject player;
    public Vector3 camForward = Vector3.zero;
    public Vector3 camRight = Vector3.zero;
    public Vector3 playerVector = Vector3.zero;
    public Vector3 playerPosition;
    public Vector3 targetPosition;
    public Vector3 camRotOffset;
    public Vector3 camLockResult;
    public TargetList targetList;
    public int currentTarget = 0;
    private float mouseWheelRaw;
    private int mouseWheel;
    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - LookTarget.position;
        targetList.GetEnemyList.enemyList[currentTarget];


    }

    private bool IsRotateActive
    {
        get
        {
            if (!RotateAroundPlayer)
                return false;

            if (!RotateMiddleMouseButton)
                return true;

            if (RotateMiddleMouseButton && Input.GetMouseButton(2))
                return true;
            if (lockedOn)
                return false;

            return false;
        }
    }
    void Update()
    {
        camForward = lookCamera.transform.TransformDirection(Vector3.forward) * 10;
        //camForward.y = 0;
        camRight = lookCamera.transform.TransformDirection(Vector3.right) * 10;
        //camRight.y = 0;
        playerVector = player.transform.position;
        Debug.DrawRay(gameObject.transform.position, playerVector - gameObject.transform.position, Color.green);
        Debug.DrawRay(transform.position, camRight, Color.red);
        Debug.DrawRay(transform.position, camForward, Color.blue);
        UpdateMouseWheel();
    }
    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        if (IsRotateActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            float h = Input.GetAxis("Mouse X") * RotationsSpeed;
            float v = Input.GetAxis("Mouse Y") * RotationsSpeed;

            Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

            Quaternion camTurnAngleY = Quaternion.AngleAxis(v, transform.right);

            Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

            // Limit camera pitch
            if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
            {
                newCameraOffset = camTurnAngle * _cameraOffset;
            }

            _cameraOffset = newCameraOffset;

        }

       
        Vector3 newPos = LookTarget.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(LookTarget);

        if (Input.GetMouseButtonDown(2))
        {
            RotateAroundPlayer = !RotateAroundPlayer;
            lockedOn = !lockedOn;
        }

        if (lockedOn)
        {
            
            transform.LookAt(enemylist[currentTarget]);
            currentTarget = currentTarget + mouseWheel;

            //_cameraOffset = newCameraOffset;
            playerPosition = player.transform.position;
            targetPosition = lockTarget.transform.position;
            camRotOffset = (playerPosition - targetPosition) / (playerPosition - targetPosition).magnitude * 4;
            camRotOffset.z += 2;
            camRotOffset.y += 2;
            //camRotoffset.y 
            Debug.DrawRay(transform.position, _cameraOffset, Color.magenta);
            Debug.DrawRay(playerPosition, camRotOffset, Color.yellow);
            camLockResult = Vector3.Slerp(_cameraOffset, camRotOffset, .1f);
            Debug.DrawRay(transform.position, camLockResult, Color.black);
            _cameraOffset = camLockResult;


        }
    }
    void UpdateMouseWheel()
    {
        mouseWheelRaw = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheelRaw > 0f)//Scroll up
        {
            mouseWheel = 1;
        }
        else if (mouseWheelRaw < 0f)//Scroll Down
        {
            mouseWheel = -1;
        }
    }
}
