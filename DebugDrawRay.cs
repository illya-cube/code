/*using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DebugDrawRay : MonoBehaviour
{
    [SerializeField] public GameObject camera;
    [SerializeField] public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camforward = camera.transform.TransformDirection(Vector3.forward) * 10;
        camforward.y = 0;
        Debug.DrawRay(transform.position, camforward, Color.red);
        Vector3 camright = camera.transform.TransformDirection(Vector3.right) * 10;
        Debug.DrawRay(transform.position, camright, Color.green);
        
        


        //Vector3 

    }
}
*/