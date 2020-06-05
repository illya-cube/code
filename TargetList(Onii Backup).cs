/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetListBackup
{
	private Vector3 thisTransform;
	private float sphereRadius;
	private Collider[] enemyList;

	public TargetList(Vector3 position, float checkRadius)
	{
		thisTransform = position;
		sphereRadius = checkRadius;
	}

	public void update()
	{
		updateEnemyList(thisTransform, sphereRadius);
	}

	public void updateEnemyList(Vector3 center, float radius)
	{
		enemyList = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Enemy"));
		int i = 0;
		while (i < enemyList.Length)
		{
			enemyList[i].SendMessage("TargetHit", true);
			i++;
		}
	}

	public Collider[] GetEnemyList()
	{
		return enemyList;
	}

	public void setPosition(Vector3 position)
	{
		thisTransform = position;
	}

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(thisTransform, sphereRadius);
	}
}
*/