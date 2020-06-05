using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList
{
	private Vector3 thisTransform;
	private float sphereRadius;
	private Collider[] enemyList;
	public int enemyListLength;
	public TargetList(Vector3 position, float checkRadius)
	{
		thisTransform = position;
		sphereRadius = checkRadius;
	}

	public void Update()
	{
		UpdateEnemyList(thisTransform, sphereRadius);
	}

	public void UpdateEnemyList(Vector3 center, float radius)
	{
		enemyList = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Enemy"));
		enemyListLength = enemyList.Length;
	}
	public void SortEnemy(Vector3 playerPosition)
	{
		if (enemyList.Length > 1)
		{
			float arrayCompare0 = (enemyList[0].gameObject.transform.position - playerPosition).magnitude;
			float arrayCompare1 = (enemyList[1].gameObject.transform.position - playerPosition).magnitude;
			if (arrayCompare0 > arrayCompare1)
			{
				Collider save = enemyList[0];
				enemyList[0] = enemyList[1];
				enemyList[1] = save;
			}
		}
	}
	public Collider[] GetEnemyList()
	{
		return enemyList;
	}

	public void SetPosition(Vector3 position)
	{
		thisTransform = position;
	}

	
}