using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string gameSceneName;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
