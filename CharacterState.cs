using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterState : MonoBehaviour
{
    public GameObject Player;
    public Movement Movement;
    public GameObject characterImage;
    public GameObject characterText;
    public Text debugJump;
    public Animator iconAnimator;
    public Animator characterAnimator;
    public int characterSelect;
    public illyaGlide illyaGlide;
    public float characterGravity;
    public float speedMod;
    public float regenMod;
    // Start is called before the first frame update
    void Start()
    {
        CheckCharacter();
        Movement = Player.GetComponent<Movement>();
        characterAnimator = Player.GetComponent<Animator>();
        illyaGlide = Player.GetComponent<illyaGlide>();
        
    }
    void SwapCharacter()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SwapIllya();
            return;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
           SwapArc();
           return;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SwapRacheal();
            return;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SwapLilith();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwapCharacter();
        debugJump.text = "Gravity = " + Movement.gravity;
    }
    void SwapIllya()
    {
        iconAnimator.SetInteger("Character", 0);
        characterAnimator.SetInteger("Character", 0);
        speedMod = 2;
        Movement.charJumps = 1;
        Movement.flyStamina = 2;
        Movement.jumpSpeed = 30;
        Movement.fallMultiplier = 4;
        Movement.charJumpHeight = 400;
        characterGravity = 40;
        illyaGlide.SetGrav();
        characterSelect = 0;
        regenMod = 400;
        return;
    }
    void SwapArc()
    {
        iconAnimator.SetInteger("Character", 1);
        characterAnimator.SetInteger("Character", 1);
        speedMod = 2;
        Movement.charJumps = 2;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 80;
        Movement.fallMultiplier = 60;
        Movement.charJumpHeight = 1200;
        characterGravity = 120;
        characterSelect = 1;
        return;
    }
    void SwapRacheal()
    {
        iconAnimator.SetInteger("Character", 2);
        characterAnimator.SetInteger("Character", 2);
        speedMod = 2;
        Movement.charJumps = 1;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 40;
        Movement.fallMultiplier = 70;
        Movement.charJumpHeight = 1000;
        characterGravity = 130;
        characterSelect = 2;
        return;
    }
    void SwapLilith()
    {
        iconAnimator.SetInteger("Character", 3);
        characterAnimator.SetInteger("Character", 3);
        speedMod = 1;
        Movement.charJumps = 1;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 200;
        Movement.fallMultiplier = 100;
        Movement.charJumpHeight = 1600;
        characterGravity = 150;
        characterSelect = 3;
        return;
    }
    public void CheckCharacter()
    {
        if(characterSelect == 0)
        {
            SwapIllya();
            return;
        }
        else if(characterSelect == 1)
        {
            SwapArc();
            return;
        }
        else if(characterSelect == 2)
        {
            SwapRacheal();
            return;
        }
        else
        {
            SwapLilith();
            return;
        }
    }
}