using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObject : MonoBehaviour

{
    public GameObject selectedObject;
    public bool highlightObject = true;
    public bool isHighlighted = false;
    public static Animator m_Animator;
    public Animator Animator;
    public RaycastInteract raycastInteract;
    public string identification;
    void Start()
    {

    }
    void Update()
    {
        flashObject();
        if (isHighlighted == true)
        {
            setTrigger();
        }
    }

    void flashObject()
    {
        selectedObject = GameObject.Find(RaycastInteract.selectedObject);
        if (RaycastInteract.lookingAtObject == true && selectedObject == gameObject)
        {
            Renderer renderer = GetComponent<Renderer>();
            Material mat = renderer.material;
            m_Animator = selectedObject.GetComponent<Animator>();

            float emission = Mathf.PingPong(Time.time, 1.0f);
            Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);
            isHighlighted = true;
        }
        else
        {
            Color initialColor = Color.black;
            Renderer renderer = GetComponent<Renderer>();
            Material mat = renderer.material;
            mat.SetColor("_EmissionColor", initialColor);
            isHighlighted = false;
        }

    }
    void setTrigger()
       
    {
        if (RaycastInteract.lookingAtObject == true && Input.GetButton("Submit"))
        {
            m_Animator.SetTrigger("didHit");
            Animator.SetTrigger("didHit");
        }
        else
        {
            m_Animator.ResetTrigger("didHit");
            Animator.ResetTrigger("didHit");
        }
    }
}