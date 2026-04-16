using UnityEngine;
using System.Collections;

public class Spawn : CheckerPlaceable
{
    public void SpawnEnemy(GameObject enemy)
    {
        GameObject instantiatedEnemy = Instantiate(enemy, GetParentChecker().transform);
        instantiatedEnemy.transform.localPosition = Vector3.zero;
        instantiatedEnemy.GetComponent<EnemyBase>().SetParentChecker(GetParentChecker());
    }
}
