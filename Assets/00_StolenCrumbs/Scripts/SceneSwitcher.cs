using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
