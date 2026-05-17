using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string gameSceneName;
    public void ChangeScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
