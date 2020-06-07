using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIState : MonoBehaviour
{
    public Slider hpSlider;
    public Text enemyName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateUI(string lockedEnemyName, float enemyHP, bool isOn, float maxHP)
    {
        if (isOn == true)
        {
            gameObject.SetActive(true);
            hpSlider.maxValue = maxHP;
            hpSlider.value = enemyHP;
            enemyName.text = lockedEnemyName;
            Debug.Log("Trying to updateUI");
        }
        else
        { 
            gameObject.SetActive(false);
    
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
