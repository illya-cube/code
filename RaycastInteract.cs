using UnityEngine;


public class RaycastInteract : MonoBehaviour
{
   //public InteractHit InteractHit;
    public static string selectedObject;
    public string internalObject;
    public RaycastHit hit;
    public static bool lookingAtObject = false;

    void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            selectedObject = hit.transform.gameObject.name;
            internalObject = hit.transform.gameObject.name;
            lookingAtObject = true;
        }
        else
        {
            lookingAtObject = false;
        }
    }
} 