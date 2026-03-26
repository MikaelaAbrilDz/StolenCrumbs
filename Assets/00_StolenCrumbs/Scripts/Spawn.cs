using UnityEngine;
using System;
using System.Collections;

public class Spawn : CheckerPlaceable
{
    [SerializeField] GameObject[] enemyPrefab;
    int counter = 0;
    void Start()
    {
        StartCoroutine(TestSpawning());
    }

    IEnumerator TestSpawning() //ONLY FOR TESTING, REBUILD THIS AFTER TESTING
    {
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(3.5f);
            SpawnEnemy(enemyPrefab[counter%2]);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject instantiatedEnemy = Instantiate(enemy, GetParentChecker().transform);
        instantiatedEnemy.GetComponent<EnemyBase>().SetParentChecker(GetParentChecker());
        instantiatedEnemy.name = "Enemy " + counter;
        counter++;
    }
}
