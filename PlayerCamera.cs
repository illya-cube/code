﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform LookTarget;
    [SerializeField] private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = .2f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public bool RotateMiddleMouseButton = false;
    public bool lockedOn = false;
    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;
    public float CameraPitchMax = 6.5f;
    //public GameObject gameObject;
    public GameObject lockTarget;
    public GameObject player;
    public GameObject crosshair;
    public Vector3 camForward = Vector3.zero;
    public Vector3 camRight = Vector3.zero;
    public Vector3 playerVector = Vector3.zero;
    public Vector3 playerPosition;
    public Vector3 targetPosition;
    public Vector3 camRotOffset;
    public Vector3 camLockResult;
    public Vector3 trueForward = Vector3.forward;
    public TargetList targetList;
    public int currentTarget = 0;
    private float mouseWheelRaw;
    private int mouseWheel;
    public Collider[] enemyList;
    public EnemyUIState uiState;
    // Use this for initialization
    void OnAwake()
    {
        playerVector = player.transform.position;
    }
    void Start()
    {
        targetList = new TargetList(gameObject.transform.position, 50f);
        _cameraOffset = transform.position - LookTarget.position;
        //targetList.GetEnemyList.enemyList[currentTarget];
    }
    //void OnDrawGizmosSelected()
    //{
        // Draw a yellow sphere at the transform's position
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(gameObject.transform.position, 50f);
    //}
    private bool IsRotateActive
    {
        get
        {
            if (RotateAroundPlayer)
                return true;
            if (lockedOn)
                return false;
            return false;
        }
    }
    /*private bool CurrentlyLocked
    {
        get
        {
        }
    }
    */
    void Update()
    {
        camForward = gameObject.transform.TransformDirection(Vector3.forward)  * 10;
        playerPosition = player.transform.position;
        camRight = gameObject.transform.TransformDirection(Vector3.right) * 10;
        playerVector = player.transform.position;
        Debug.DrawRay(gameObject.transform.position, playerVector - gameObject.transform.position, Color.green);
        Debug.DrawRay(transform.position, camRight, Color.red);
        Debug.DrawRay(transform.position, camForward, Color.blue);
        targetList.SetPosition(gameObject.transform.position);
        targetList.Update();
        // targetList.SortEnemy(playerPosition);

        enemyList = targetList.GetEnemyList();
        //Debug.Log(enemyList[0].gameObject.name + "  " + (enemyList[0].gameObject.transform.position - playerPosition).magnitude);
        //Debug.Log(enemyList[1].gameObject.name + "  " + (enemyList[1].gameObject.transform.position - playerPosition).magnitude);
    }
    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        crosshair.SetActive(lockedOn);


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

                //inside this if statement, use trueForward to slerp camera offset back to neutral position
            }
            if (newCameraOffset.y < CameraPitchMin)
            {

                updateCamera(0);
            }
            if (newCameraOffset.y > CameraPitchMax)
            {
                updateCamera(1);
            }
            
            _cameraOffset = newCameraOffset;
            void updateCamera(int resetValue)
            {
                if (resetValue == 0)
                { 
                    newCameraOffset.y = CameraPitchMin;
                }
                if (resetValue == 1)
                {
                    newCameraOffset.y = CameraPitchMax;
                }
            }
        }


        Vector3 newPos = LookTarget.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(LookTarget);
        if (enemyList.Length == 0)
        {
            lockedOn = false;
            RotateAroundPlayer = true;
        }
        if (Input.GetMouseButtonDown(2) && enemyList.Length != 0)
        {
            RotateAroundPlayer = !RotateAroundPlayer;
            lockedOn = !lockedOn;
        }
        if (!lockedOn)
        {
           currentTarget = 0;
        }
        if (lockedOn)
        {
            UpdateMouseWheel();
            /*
            if (currentTarget >= (enemyList.Length - 1) && mouseWheel > 0) // if 3 = 3 and input > 0, subtract to get 0 (minimum) cycles above the array to get to the bottom
            {
                currentTarget -= enemyList.Length;
            }
            else if (currentTarget == 0 && mouseWheel < 0)//if 0 = 0 and input < 0, add to get to enemyList.Length (max)  cycles below the array to get to the top
            {
                currentTarget += enemyList.Length;
            }
            if (enemyList.Length - 1 < currentTarget && currentTarget >= 1)
            {
                currentTarget -= 1;
            }
            */

            //var targetEnemy = enemyList[currentTarget];
            lockTarget = SetEnemy(mouseWheel);
            
            if (lockTarget != null)
            {
                transform.LookAt(lockTarget.transform.position);
            }


            //_cameraOffset = newCameraOffset;

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
            uiState.UpdateUI(lockTarget.GetComponent<EnemyStats>().myName, lockTarget.GetComponent<EnemyStats>().currentHealth, true, lockTarget.GetComponent<EnemyStats>().maxHealth);


        }

    }
    public GameObject SetEnemy(int wheel)
    {

        if (lockTarget == null)
        {
            currentTarget = 0;
            if (enemyList.Length > 0)
            {
                return enemyList[0].gameObject;
            }
            else
            {
                return null;
            }
        }
        
        if (Vector3.Distance(lockTarget.transform.position, playerPosition) > 50f)
        {
            
            if (enemyList.Length > 0)
            {
                return enemyList[0].gameObject;
            }
            else
            {
                return null;
            }
        }
        if (wheel != 0)
        {
            currentTarget = currentTarget + mouseWheel;
            if (currentTarget > enemyList.Length - 1)
            {
                currentTarget = 0;
            }
            else if (currentTarget < 0)
            {
                currentTarget = enemyList.Length - 1;
            }
            return enemyList[currentTarget].gameObject;
        }
        else
        {
            return lockTarget;
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
        else
        {
            mouseWheel = 0;
        }
    }
}