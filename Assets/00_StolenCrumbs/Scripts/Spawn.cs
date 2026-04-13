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
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(7f);
            SpawnEnemy(enemyPrefab[counter%2]);
        }
        WinLoseManager._isFinalWave = true;
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject instantiatedEnemy = Instantiate(enemy, GetParentChecker().transform);
        instantiatedEnemy.transform.localPosition = Vector3.zero;
        instantiatedEnemy.GetComponent<EnemyBase>().SetParentChecker(GetParentChecker());
        instantiatedEnemy.name = "Enemy " + counter;
        counter++;
    }
}
