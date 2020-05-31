using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllyaFlyBar : MonoBehaviour
{
    public Slider illyaFlyBar;
    public Movement movement;
    public static GameObject illyaKUI;
    public Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
        illyaKUI = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        illyaFlyBar.value = Movement.flyStamina;


    }
}
