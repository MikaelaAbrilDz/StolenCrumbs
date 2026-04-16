using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject SoundMenuManager;
    public InputActionReference pauseAction;
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
            Time.timeScale = 1f;
        }
        else
        {
            SoundMenuManager.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
