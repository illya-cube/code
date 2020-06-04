using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{
    private Vector3 thisTransform;
    public float sphereRadius;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnAwake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //GetEnemyList(thisTransform, sphereRadius);
        thisTransform = gameObject.transform.position;

    }
    public void GetEnemyList(Vector3 center, float radius)
    {
        Collider[] enemyList = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Enemy"));
        int i = 0;
        while (i < enemyList.Length)
        {
            enemyList[i].SendMessage("TargetHit", true);
            i++;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}
