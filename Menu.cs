using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool isMenu;
    // Start is called before the first frame update
    void Start()
    {
        isMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            this.gameObject.SetActive(true);
        }
    }
}
