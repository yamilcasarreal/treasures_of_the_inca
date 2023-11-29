using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public GameObject Spawner;
    void Start()
    {
        StartCoroutine(EnemyDrop());
        //Spawner = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < 4)
        {
            xPos = Random.Range((int)Spawner.transform.position.x, (int)Spawner.transform.position.x + 20);
            zPos = Random.Range((int)Spawner.transform.position.z, (int)Spawner.transform.position.z + 20);
            Instantiate(Enemy, new Vector3(xPos, 0, zPos), Quaternion.identity );
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
