using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    public List<Spawn> spawns = new List<Spawn>();

    struct EnemiesToBeUsed
    {
        public EnemiesToBeUsed(int lenght, int[] enemies, float[] frequencies)
        {
            this.lenght = lenght;
            this.enemies = enemies;
            this.frequencies = new float[frequencies.Length];
            float frequencyCounter = 0;
            for (int i = 0; i < frequencies.Length; i++)
            {
                if (i == frequencies.Length - 1)
                {
                    this.frequencies[i] = 1 - frequencyCounter;
                    break;
                }
                frequencyCounter += frequencies[i];
                if (frequencyCounter <= 1)
                {
                    this.frequencies[i] = frequencies[i];
                }
                else
                {
                    this.frequencies[i] = frequencies[i] - (frequencyCounter - 1);
                    frequencyCounter = 1;
                }
            }
        }
        public int lenght;
        public int[] enemies;
        public float[] frequencies;
    }
    void Start()
    {
        StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(45, new int[]{ 0, 1 }, new float[] { 0.8f, 0.2f }))));
    }
    int[] GenerateEnemyQueue(EnemiesToBeUsed enemies)
    {
        int[] enemyQueue = new int[enemies.lenght];

        for (int i = 0; i < enemyQueue.Length; i++)
        {
            float value = Random.value;
            float offset = 0;
            for (int j = 0; j < enemies.frequencies.Length; j++)
            {
                print(value + " vs " + (enemies.frequencies[j] + offset));
                if (value <= enemies.frequencies[j] + offset)
                {
                    enemyQueue[i] = enemies.enemies[j];
                    print("CHANGED VALUE");
                    break;
                }
                offset += enemies.frequencies[j];
            }
        }

        return enemyQueue;
    }
    IEnumerator SpawnCo(int[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            print(enemyPrefab[enemies[i]].name);
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(3f);
            spawns[Random.Range(0, spawns.Count)].SpawnEnemy(enemyPrefab[enemies[i]]);
        }
        WinLoseManager._isFinalWave = true;
    }
}
