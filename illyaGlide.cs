using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illyaGlide : MonoBehaviour
{
    
    public Movement movement;
    public CharacterState charState;
    public IllyaFlyBar flyUI;
    public bool canFly;
    // Start is called before the first frame update
    void Start()
    {
       movement = gameObject.GetComponent<Movement>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGlide();
        Glide();
    }
    void CheckForGlide() //this function checks movement if we are in the air, and if we have enough stamina to fly
    {
        if(movement.inAir == true && Movement.flyStamina > 0)
        {
            canFly = true;
            //Debug.Log("i can fly!");
        }
        else
        {
            canFly = false;
        }
    }
    void Glide()
    {
        if(canFly = true && Input.GetButton("Jump"))
        {
            Debug.Log("I am trying to fly!");
        }
    }
}
