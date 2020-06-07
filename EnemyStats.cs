using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string myName;
    public float maxHealth;
    public float currentHealth;
    public int level;
    //public EnemyZone enemyZone;
    private GameObject me;
    public EnemyUIState uiState;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        me = gameObject;
        myName = gameObject.name;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void CheckHealth(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Destroy(me);
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
        Debug.Log(myName + " has been hit!");
        Debug.Log(myName + "hit for " + damageValue);
        Debug.Log("Health Left: " + currentHealth);
        CheckHealth(damageValue);

    }
    public void UpdateEnemyStats(float enemyMinLevel, float enemyMaxLevel, int enemyDefence)
    {
        //enemyName = gameObject.name;
        //Mathf.RoundToInt
        int min = Mathf.RoundToInt(enemyMinLevel);
        int max = Mathf.RoundToInt(enemyMaxLevel);
        maxHealth = Random.Range(min, max * enemyDefence);
        currentHealth = maxHealth;
    }
}

