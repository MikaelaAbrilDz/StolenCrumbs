using EPOOutline.Demo;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class PathOrigin : CheckerPlaceable
{
    [SerializeField] int initialDirection;
    int currentDirection;
    float desviation = 3f;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject pathPrefab;
    CheckerManager currentChecker;
    int steps = 0;

    List<GameObject> paths = new List<GameObject>();
    
    public bool GeneratePath()
    {
        paths.Clear();

        currentChecker = parentChecker.sideCheckers[initialDirection];
        currentDirection = initialDirection;
        float currentDesviation = desviation;

        while (currentChecker.GetComponentsInChildren<Limit>().Length == 0)
        {
            GameObject placedPath = Instantiate(pathPrefab, currentChecker.transform);
            placedPath.GetComponent<Path>().SetParentChecker(currentChecker);
            paths.Add(placedPath);

            int nextDirection = -1;
            int directionCounter = 25;
            while (nextDirection < 0 && directionCounter > 0)
            {
                nextDirection = GenerateDirection(currentDesviation);
                directionCounter--;
            }

            if (nextDirection == -1)
            {
                print("PATH GENERATION WENT WRONG");
                foreach (GameObject path in paths)
                {
                    Destroy(path);
                }
                return false;
            }

            SetPath(placedPath.GetComponent<Path>(), nextDirection);

            currentChecker = currentChecker.sideCheckers[nextDirection];
            currentDirection = nextDirection;
            steps++;
            currentDesviation *= 0.9f;
        }

        GameObject placedPathFinal = Instantiate(pathPrefab, currentChecker.transform);
        placedPathFinal.GetComponent<Path>().SetParentChecker(currentChecker);

        SetPath(placedPathFinal.GetComponent<Path>(), currentDirection);

        GameObject placedSpawn = Instantiate(spawnPrefab, currentChecker.transform);
        placedSpawn.GetComponent<Spawn>().SetParentChecker(currentChecker);
        FindAnyObjectByType<SpawnManager>().spawns.Add(placedSpawn.GetComponent<Spawn>());

        return true;
    }

    private int GenerateDirection(float currentDesviation)
    {
        int value = -1;
        while (value < 0)
        {
            value = GetClampedDirection(currentDesviation);
        }

        if (!currentChecker.GetComponent<CheckerManager>().isPathPlaceable || currentChecker.sideCheckers[value].GetComponentInChildren<Path>() || currentChecker.sideCheckers[value].GetComponentInChildren<Fort>() || currentChecker.sideCheckers[value].GetComponentInChildren<Turret>())
        {
            return -1;
        }

        return value;
    }

    private int GetClampedDirection(float currentDesviation)
    {
        int baseDirection = initialDirection;

        float randNormal = Mathf.Sqrt(-2f * Mathf.Log(Random.Range(0f, 1f))) * Mathf.Sin(2f * Mathf.PI * Random.Range(0f, 1f));


        int value = Mathf.Clamp((baseDirection + Mathf.RoundToInt(currentDesviation * randNormal)), -5, 11);
        int range = 6;
        value = (value) % range;
        if (value < 0) value += range;

        if (Mathf.Abs(currentDirection - value) < 2 || Mathf.Abs(currentDirection - value) > 5)
        {
            return value;
        }
        else
        {
            return -1;
        }


    }
    private void SetPath(Path path, int nextDirection)
    {
        path.SetPath(currentDirection, nextDirection, steps);
    }
}
