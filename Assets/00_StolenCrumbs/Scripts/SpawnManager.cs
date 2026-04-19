using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    public List<Spawn> spawns = new List<Spawn>();

    public List<Round> rounds = new List<Round>();

    int round = 0;

    struct EnemiesToBeUsed
    {
        public EnemiesToBeUsed(int lenght, Round.EnemyType[] enemies, float[] frequencies)
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
        public Round.EnemyType[] enemies;
        public float[] frequencies;
    }
    void Start()
    {
        StartCoroutine(SpawnGenerator());
    }
    IEnumerator SpawnGenerator()
    {
        yield return new WaitForSeconds(3);
        if (round < rounds.Count)
        {
            StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(rounds[round].numberOfEnemies, rounds[round].enemies, rounds[round].enemiesProbs)), rounds[round].rate));
        }
        else
        {
            WinLoseManager._isFinalWave = true;
        }
    }
    Round.EnemyType[] GenerateEnemyQueue(EnemiesToBeUsed enemies)
    {
        Round.EnemyType[] enemyQueue = new Round.EnemyType[enemies.lenght];

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
    IEnumerator SpawnCo(Round.EnemyType[] enemies, float delay)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            spawns[Random.Range(0, spawns.Count)].SpawnEnemy(enemyPrefab[(int)enemies[i]]);
        }
        round++;
        StartCoroutine(SpawnGenerator());
    }
}
