using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    public List<Spawn> spawns = new List<Spawn>();
    void Start()
    {
        StartCoroutine(SpawnCo(new int[]{0,1,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0}));
    }
    IEnumerator SpawnCo(int[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(3f);
            spawns[Random.Range(0, spawns.Count)].SpawnEnemy(enemyPrefab[enemies[i]]);
        }
        WinLoseManager._isFinalWave = true;
    }
}
