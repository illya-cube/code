using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCapture : MonoBehaviour
{
	public Menu menuCall;
	public Transform pivotTransform;
	private Vector3 _cameraOffset;
	[Range(0.01f, 1.0f)] public float SmoothFactor = 0.5f;
	public bool LookAtPlayer = false;
	public bool RotateAroundPlayer = true;
	[Range(0.01f, 10f)] public float RotationsSpeed = 5.0f;

	
	void Start() 
	{
		_cameraOffset = transform.position - pivotTransform.position;
	}


	// called after update methods
	void LateUpdate() 
	{
		if(RotateAroundPlayer == true)
        {
			Quaternion camTurnAngle = 
			Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
			camTurnAngle *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, Vector3.right);
			_cameraOffset = camTurnAngle * _cameraOffset;
			Cursor.lockState = CursorLockMode.Locked;
			Debug.Log(_cameraOffset);
        }
		else
        {
			MenuUncapture();
        }

		Vector3 newPos = pivotTransform.position + _cameraOffset;

		transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

		if (LookAtPlayer || RotateAroundPlayer)
			transform.LookAt(pivotTransform);
	}

	void MenuUncapture()
    {
		if (Menu.isMenu == true) 
		{
			Cursor.lockState = CursorLockMode.None;
		}
    }
}