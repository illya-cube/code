using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public int thisZone;
    public GameObject PinkCube;
    public GameObject RedCube;
    public GameObject Zone0;
    public GameObject Zone99;
    private Vector3 Zone0Min;
    private Vector3 Zone0Max;
    private Vector3 Zone99min;
    private Vector3 Zone99max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

    }
    
    /*void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Zone1size = Zone1.GetComponent<Collider>().bounds.size;
            Zone99size = Zone99.GetComponent<Collider>().bounds.size;
            Debug.Log("x=" + Zone1size.x);
            SetZone(thisZone);
        }
    }*/
    
        
    /*void EnemyType(int enemyID)
    {
        switch(enemyID)
        {

        }
    }*/
    public void SetZone(int areaZone)
    {
       
       
        //Debug.Log("x=" + Zone1size.x + "y" + Zone1size.y + "z" + Zone1size.z);
        //Debug.Log("x=" + Zone99size.x + "y" + Zone99size.y + "z" + Zone99size.z);
        

        switch (areaZone)
        {
            case 0:
                Zone0Min = Zone0.GetComponent<Collider>().bounds.min;
                Zone0Max = Zone0.GetComponent<Collider>().bounds.max;
                //Vector3 zone0RangeMin = new Vector3(-70f, 0f,-70f);
                //Vector3 zone0RangeMax = new Vector3(70f, 0f, 70f);
                Vector2 zone0LevelRange = new Vector2(1, 5);
                print("You are in zone 0");
                //enemyStats.UpdateEnemyStats(1,5);
                StartCoroutine(SpawnEnemy(1, PinkCube, Zone0Min, Zone0Max, zone0LevelRange, 1));
                break;
            case 1:
                print("You are in zone 1");
                break;
            case 2:
                print("z2");
                break;
            case 3:
                print("z3");
                break;
            case 4:
                print("z4");
                break;
            case 99:
                Zone99min = Zone99.GetComponent<Collider>().bounds.min;
                Zone99max = Zone99.GetComponent<Collider>().bounds.max;
                //Vector3 zone99RangeMin = new Vector3(-345.7f, 0f, -70f);
                //Vector3 zone99RangeMax = new Vector3(-200f, 0f, 70f);
                Vector2 zone99LevelRange = new Vector2(1, 5);
                print("welcome to hell, i shall be your guide");
                StartCoroutine(SpawnEnemy(10, RedCube, Zone99min, Zone99max, zone99LevelRange, 9));
                StartCoroutine(SpawnEnemy(1, PinkCube, Zone99min, Zone99max, zone99LevelRange, 1));
                break;
            default:
                print("error - no zone set");
                break;
        }
    }
    IEnumerator SpawnEnemy(int spawnAmount, GameObject enemyType, Vector3 rangeMin, Vector3 rangeMax, Vector2 levelRange, int enemyDefence)
    {
        int enemyCount = 0;
        while (enemyCount < spawnAmount)
        {
            float xPos = Random.Range (rangeMin.x, rangeMax.x);
            float zPos = Random.Range (rangeMin.z, rangeMax.z);
            GameObject thisEnemy = Instantiate(enemyType, new Vector3(xPos,16,zPos), Quaternion.identity);
            thisEnemy.SetActive(true);
            thisEnemy.GetComponent<EnemyStats>().UpdateEnemyStats(levelRange.x, levelRange.y,enemyDefence);
            enemyCount += 1;
            yield return new WaitForSeconds(Random.value);
        }
        yield return null;
    }
}
