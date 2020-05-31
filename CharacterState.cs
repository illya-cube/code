using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterState : MonoBehaviour
{
    public Movement Movement;
    public GameObject characterImage;
    public GameObject characterText;
    public Text debugJump;
    public Animator iconAnimator;
    public Animator characterAnimator;
    public int characterSelect;
    // Start is called before the first frame update
    void Start()
    {
        CheckCharacter();
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
        debugJump.text = "Jump Speed = " + Movement.jumpSpeed;
    }
    void SwapIllya()
    {
        iconAnimator.SetInteger("Character", 0);
        characterAnimator.SetInteger("Character", 0);
        //Debug.Log("i'm Illya Now!");
        characterSelect = 0;
        Movement.charJumpMod = 0;
        Movement.flyStamina = 200;
        Movement.jumpSpeed = 40;
        Movement.fallMultiplier = 20;
        return;
    }
    void SwapArc()
    {
        iconAnimator.SetInteger("Character", 1);
        characterAnimator.SetInteger("Character", 1);
       //Debug.Log("i'm Arc now!");
        characterSelect = 1;
        Movement.charJumpMod = 2;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 80;
        Movement.fallMultiplier = 60;
        return;
    }
    void SwapRacheal()
    {
        iconAnimator.SetInteger("Character", 2);
        characterAnimator.SetInteger("Character", 2);
        //Debug.Log("i'm Racheal now!");
        characterSelect = 2;
        Movement.charJumpMod = 0;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 40;
        Movement.fallMultiplier = 70;
        return;
    }
    void SwapLilith()
    {
        iconAnimator.SetInteger("Character", 3);
        characterAnimator.SetInteger("Character", 3);
        //Debug.Log("i'm Lilith now!");
        characterSelect = 3;
        Movement.charJumpMod = 1;
        Movement.flyStamina = 0;
        Movement.jumpSpeed = 200;
        Movement.fallMultiplier = 100;
        return;
    }
    void CheckCharacter()
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