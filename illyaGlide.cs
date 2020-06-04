using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illyaGlide : MonoBehaviour
{
    
    public Movement movement;
    public CharacterState charState;
    public IllyaFlyBar flyUI;
    public bool canGlide = false;
    public GameObject wingParticle;
    public float i;
    public float previousFallMult;
    public float previousGravity;
    public bool startGlide;
    
    // Start is called before the first frame update
    void Start()
    {
       movement = gameObject.GetComponent<Movement>(); 
       SetGrav();
    }

    // Update is called once per frame
    void Update()
    {
        FlyTimer();
        Glide();
        
    }
    public void FlyTimer()
    {
        if(movement.inAir == true && Input.GetButton("Jump") && startGlide == true)
        { 
            i += Time.deltaTime;
            if( i > .3)
            {
                canGlide = true;
            }
        }
        else
        {
            canGlide = false; 
            i =0;
        }
    }
    void Glide()
    {
        if(canGlide == true && Input.GetButton("Jump"))
        {
            Debug.Log("I am trying to glide!");
            wingParticle.SetActive(true);
            Movement.fallMultiplier = 0;
            Movement.gravity = 10;
        }
        else  
        {
            wingParticle.SetActive(false);
            Movement.fallMultiplier = 20;
            Movement.fallMultiplier = previousFallMult;
            Movement.gravity = previousGravity;
        }
    }
    public void SetGrav()
    {
        previousFallMult = Movement.fallMultiplier;
        previousGravity = Movement.gravity;
    }
}
