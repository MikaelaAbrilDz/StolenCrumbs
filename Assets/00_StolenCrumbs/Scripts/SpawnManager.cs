using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject roundPassObj;
    public List<Spawn> spawns = new List<Spawn>();

    public List<Round> firstWaveRounds = new List<Round>();
    public List<Round> secondWaveRounds = new List<Round>();
    public List<Round> thirdWaveRounds = new List<Round>();
    public List<Round> fourthWaveRounds = new List<Round>();
    public List<Round> fifthWaveRounds = new List<Round>();
    public List<Round> sixthWaveRounds = new List<Round>();

    int round = 0;
    int wave = 1;

    int roundPrize = 30;

    [SerializeField] AudioClip earnMoneySound;
    [SerializeField] TextMeshProUGUI currentWaveText;

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
        CurrencyManager.SetCurrency(40);
        TimeScaleManager.SetTime(1f);
        FindAnyObjectByType<TurretUnlockManager>().ShowUnlockOptions();
        StartCoroutine(SpawnGenerator());
    }
    IEnumerator SpawnGenerator()
    {
        yield return new WaitForSeconds(3);
        if (wave == 1)
        {
            currentWaveText.text = "Wave 1";
            if (round < firstWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(firstWaveRounds[round].numberOfEnemies, firstWaveRounds[round].enemies, firstWaveRounds[round].enemiesProbs)), firstWaveRounds[round].rate));
            else
            {
                round = 0;
                wave++;
                while(FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
                SFXManager sfx = FindAnyObjectByType<SFXManager>();
                sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
                CurrencyManager.AddCurrency(roundPrize);
                if (FindAnyObjectByType<SFXManager>() != null)
                {
                    FindAnyObjectByType<SFXManager>().PlaySoundFXClip(earnMoneySound, transform, 1f);
                }
                FindAnyObjectByType<TurretUnlockManager>().ShowUnlockOptions();
                FindAnyObjectByType<PathGenerationManager>().CreatePath();
            } 
        }
        if (wave == 2)
        {
            currentWaveText.text = "Wave 2";
            if (round < secondWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(secondWaveRounds[round].numberOfEnemies, secondWaveRounds[round].enemies, secondWaveRounds[round].enemiesProbs)), secondWaveRounds[round].rate));
           else
           {
                round = 0;
                wave++;
                while (FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
                SFXManager sfx = FindAnyObjectByType<SFXManager>();
                sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
                CurrencyManager.AddCurrency(roundPrize);
                if (FindAnyObjectByType<SFXManager>() != null)
                {
                    FindAnyObjectByType<SFXManager>().PlaySoundFXClip(earnMoneySound, transform, 1f);
                }
                FindAnyObjectByType<TurretUnlockManager>().ShowUnlockOptions();
            }
        }
        if (wave == 3)
        {
            currentWaveText.text = "Wave 3";
            if (round < thirdWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(thirdWaveRounds[round].numberOfEnemies, thirdWaveRounds[round].enemies, thirdWaveRounds[round].enemiesProbs)), thirdWaveRounds[round].rate));
           else
           {
                round = 0;
                wave++;
                while (FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
                SFXManager sfx = FindAnyObjectByType<SFXManager>();
                sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
                CurrencyManager.AddCurrency(roundPrize);
                if (FindAnyObjectByType<SFXManager>() != null)
                {
                    FindAnyObjectByType<SFXManager>().PlaySoundFXClip(earnMoneySound, transform, 1f);
                }
                FindAnyObjectByType<TurretUnlockManager>().ShowUnlockOptions();
                FindAnyObjectByType<PathGenerationManager>().CreatePath();
            }
        }
        if (wave == 4)
        {
            currentWaveText.text = "Wave 4";
            if (round < fourthWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(fourthWaveRounds[round].numberOfEnemies, fourthWaveRounds[round].enemies, fourthWaveRounds[round].enemiesProbs)), fourthWaveRounds[round].rate));
           else
           {
                round = 0;
                wave++;
                while (FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
                SFXManager sfx = FindAnyObjectByType<SFXManager>();
                sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
                CurrencyManager.AddCurrency(roundPrize);
                if (FindAnyObjectByType<SFXManager>() != null)
                {
                    FindAnyObjectByType<SFXManager>().PlaySoundFXClip(earnMoneySound, transform, 1f);
                }
                FindAnyObjectByType<TurretUnlockManager>().ShowUnlockOptions();
            }
        }
        if (wave == 5)
        {
            currentWaveText.text = "Wave 5";
            if (round < fifthWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(fifthWaveRounds[round].numberOfEnemies, fifthWaveRounds[round].enemies, fifthWaveRounds[round].enemiesProbs)), fifthWaveRounds[round].rate));
           else
           {
                round = 0;
                wave++;
                while (FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
                SFXManager sfx = FindAnyObjectByType<SFXManager>();
                sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
                CurrencyManager.AddCurrency(roundPrize);
                FindAnyObjectByType<PathGenerationManager>().CreatePath();
            }
        }
        if (wave == 6)
        {
            currentWaveText.text = "Final wave";
            if (round < sixthWaveRounds.Count) StartCoroutine(SpawnCo(GenerateEnemyQueue(new EnemiesToBeUsed(sixthWaveRounds[round].numberOfEnemies, sixthWaveRounds[round].enemies, sixthWaveRounds[round].enemiesProbs)), sixthWaveRounds[round].rate));
           else
           {
                round = 0;
                wave++;
                while (FindAnyObjectByType<EnemyBase>() != null) yield return new WaitForEndOfFrame();
                roundPassObj.SetActive(true);
                yield return new WaitForSeconds(2);
                roundPassObj.SetActive(false);
            }
        }
        if (wave == 7)
        {
            WinLoseManager._gameState = WinLoseManager.GameState.Won;
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
