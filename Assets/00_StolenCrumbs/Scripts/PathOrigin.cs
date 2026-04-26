using Unity.VisualScripting;
using UnityEngine;

public class PathOrigin : CheckerPlaceable
{
    [SerializeField] int initialDirection;
    int currentDirection;
    int desviation = 1;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject pathPrefab;
    CheckerManager currentChecker;
    int steps = 0;
    public void GeneratePath()
    {
        currentChecker = parentChecker.sideCheckers[initialDirection];
        currentDirection = initialDirection;

        while (currentChecker.GetComponentsInChildren<Limit>().Length == 0)
        {
            GameObject placedPath = Instantiate(pathPrefab, currentChecker.transform);
            placedPath.GetComponent<Path>().SetParentChecker(currentChecker);

            int nextDirection = GenerateDirection();

            SetPath(placedPath.GetComponent<Path>(), nextDirection);

            currentChecker = currentChecker.sideCheckers[nextDirection];
            currentDirection = nextDirection;
            steps++;
        }

        GameObject placedPathFinal = Instantiate(pathPrefab, currentChecker.transform);
        placedPathFinal.GetComponent<Path>().SetParentChecker(currentChecker);

        SetPath(placedPathFinal.GetComponent<Path>(), currentDirection);

        GameObject placedSpawn = Instantiate(spawnPrefab, currentChecker.transform);
        placedSpawn.GetComponent<Spawn>().SetParentChecker(currentChecker);
        FindAnyObjectByType<SpawnManager>().spawns.Add(placedSpawn.GetComponent<Spawn>());
    }

    private int GenerateDirection()
    {
        int baseDirection = initialDirection;

        float randNormal = Mathf.Sqrt(-2f * Mathf.Log(Random.Range(0f, 1f))) * Mathf.Sin(2f * Mathf.PI * Random.Range(0f, 1f));
        

        int value = baseDirection + Mathf.RoundToInt(desviation * randNormal);
        int range = 5 + 1;
        value = (value) % range;
        if (value < 0) value += range;


        if (currentChecker.sideCheckers[value].GetComponentInChildren<Path>() || currentChecker.sideCheckers[value].GetComponentInChildren<Fort>() || currentChecker.sideCheckers[value].GetComponentInChildren<Turret>())
        {
            value = GenerateDirection();
        }

        return value;
    }
    private void SetPath(Path path, int nextDirection)
    {
        path.SetPath(currentDirection, nextDirection, steps);
    }
}
