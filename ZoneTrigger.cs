using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public int thisZone;
    public EnemyZone enemyZone;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyZone.SetZone(thisZone);
        }
    }
}
