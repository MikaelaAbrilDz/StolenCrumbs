using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SoundsMenuManager : MonoBehaviour
{
    public GameObject SoundMenuManager;
    public InputActionReference pauseAction;

    [SerializeField] string gameSceneName;
    public void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
    }
    
    private void OnPause(InputAction.CallbackContext context)
    {
        if (SoundMenuManager.activeSelf)
        {
            SoundMenuManager.SetActive(false);
            TimeScaleManager.UnPauseTime();
        }
        else
        {
            SoundMenuManager.SetActive(true);
            TimeScaleManager.PauseTime();
        }
    }

    public void ComeBack()
    {
        SoundMenuManager.SetActive(false);
        TimeScaleManager.UnPauseTime();
    }
}
