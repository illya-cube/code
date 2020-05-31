﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    [SerializeField] private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public bool RotateMiddleMouseButton = true;
    public float RotationsSpeed = 5.0f;
    public float CameraPitchMin = 1.5f;
    public float CameraPitchMax = 6.5f;
    public GameObject lookCamera;
    public GameObject character;
    public Vector3 camForward = Vector3.zero;
    public Vector3 camRight = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
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

            return false;
        }
    }
    void Update()
    {
        camForward = lookCamera.transform.TransformDirection(Vector3.forward) * 10;
        camForward.y = 0;
        camRight = lookCamera.transform.TransformDirection(Vector3.right) * 10;
        camRight.y = 0;

        
        //Debug.DrawRay(character.transform.position, camForward, Color.red);
        //Debug.DrawRay(character.transform.position, camRight, Color.blue);
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

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
    }
}