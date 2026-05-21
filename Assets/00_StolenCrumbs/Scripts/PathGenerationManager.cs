using UnityEngine;

public class PathGenerationManager : MonoBehaviour
{
    [SerializeField] PathOrigin[] origin;
    int originIndex = -1;

    private void Start()
    {
        CreatePath();
    }
    public void CreatePath()
    {
        if (originIndex == -1) originIndex = Random.Range(0, origin.Length);
        else originIndex = (originIndex + Random.Range(1, 3)) % origin.Length;

        print(originIndex);

        origin[originIndex].GeneratePath(35);
    }
}
