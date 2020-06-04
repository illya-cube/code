using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyName;
    public int enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TargetHit(bool wasHit)
    {
        if (wasHit)
        {
            Debug.Log(enemyName + " is on the target list");
        }
    }
    public void DamageHit(bool wasHit, int damageValue)
    {
        if (wasHit == true)
        {
            ReportDamage(damageValue);
        }

    }
    public void ReportDamage(int damageValue)
    {
        Debug.Log(enemyName + "  has been hit!");
        Debug.Log(enemyName + "hit for " + damageValue);
    }
}
