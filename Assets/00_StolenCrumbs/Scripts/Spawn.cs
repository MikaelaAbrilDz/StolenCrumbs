using UnityEngine;
using System;
using System.Collections;

public class Spawn : CheckerPlaceable
{
    [SerializeField] GameObject enemyPrefab;
    int counter = 0;
    void Start()
    {
        StartCoroutine(TestSpawning());
    }

    IEnumerator TestSpawning() //ONLY FOR TESTING, REBUILD THIS AFTER TESTING
    {
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(3);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(2);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(2);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(5);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(0.7f);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(0.7f);
        SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(0.7f);
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject instantiatedEnemy = Instantiate(enemy, GetParentChecker().transform);
        instantiatedEnemy.GetComponent<EnemyBase>().SetParentChecker(GetParentChecker());
        instantiatedEnemy.name = "Enemy " + counter;
        counter++;
    }
}
