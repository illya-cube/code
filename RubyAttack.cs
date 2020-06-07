using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyAttack : MonoBehaviour
{
    public GameObject ruby;
    public ParticleSystem sprinklerAttack;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("particle hit a collider!");
        if (other.tag == "Enemy")
        {
            //Debug.Log("particle hit a enemy!");
            EnemyStats enemyStat;
            enemyStat = other.GetComponent<EnemyStats>();
            enemyStat.ReportDamage(1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
