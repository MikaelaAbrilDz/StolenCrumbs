using UnityEngine;

public class PathGenerationManager : MonoBehaviour
{
    [SerializeField] PathOrigin[] origin;

    private void Start()
    {
        CreatePath();
    }
    public void CreatePath()
    {
        origin[Random.Range(0, origin.Length)].GeneratePath();
    }
}
