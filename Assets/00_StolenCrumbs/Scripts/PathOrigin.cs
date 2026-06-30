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
    
    public void GeneratePath(int fullCounter)
    {
        if (fullCounter == 0)
        {
            print("PATH GENERATION WAS IMPOSSIBLE");
            return;
        }

        paths.Clear();

        currentChecker = parentChecker.sideCheckers[initialDirection];
        currentDirection = initialDirection;
        float currentDesviation = desviation;

        while (currentChecker.GetComponentsInChildren<Limit>().Length == 0)
        {
            GameObject placedPath = Instantiate(pathPrefab, currentChecker.transform);
            placedPath.GetComponent<Path>().SetParentChecker(currentChecker);
            paths.Add(placedPath);

            int nextDirection = GenerateDirection(fullCounter, 25, currentDesviation);

            if (nextDirection == -1)
            {
                print("PATH GENERATION WENT WRONG");
                return;
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
    }

    private int GenerateDirection(int fullCounter, int counter, float currentDesviation)
    {
        if (counter == 0)
        {
            foreach (GameObject path in paths)
            {
                Destroy(path);
            }
            GeneratePath(fullCounter - 1);
            return -1;
        }

        int value = GetClampedDirection(fullCounter, counter, currentDesviation);

        if (value == -1)
        {
            GeneratePath(fullCounter - 1);
            return value;
        }


        if (!currentChecker.GetComponent<CheckerManager>().isPathPlaceable || currentChecker.sideCheckers[value].GetComponentInChildren<Path>() || currentChecker.sideCheckers[value].GetComponentInChildren<Fort>() || currentChecker.sideCheckers[value].GetComponentInChildren<Turret>())
        {
            value = GenerateDirection(fullCounter, counter - 1, currentDesviation);
        }

        return value;
    }

    private int GetClampedDirection(int fullCounter, int counter, float currentDesviation)
    {
        int baseDirection = initialDirection;

        float randNormal = Mathf.Sqrt(-2f * Mathf.Log(Random.Range(0f, 1f))) * Mathf.Sin(2f * Mathf.PI * Random.Range(0f, 1f));


        int value = baseDirection + Mathf.RoundToInt(currentDesviation * randNormal);
        int range = 6;
        value = (value) % range;
        if (value < 0) value += range;

        if (Mathf.Abs(currentDirection - value) < 2 || Mathf.Abs(currentDirection - value) > 5)
        {
            return value;
        }
        else
        {
            return GenerateDirection(fullCounter, counter - 1, currentDesviation);
        }


    }
    private void SetPath(Path path, int nextDirection)
    {
        path.SetPath(currentDirection, nextDirection, steps);
    }
}
