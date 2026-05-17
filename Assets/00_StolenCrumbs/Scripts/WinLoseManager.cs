using UnityEngine;
using System.Collections;

public class WinLoseManager : MonoBehaviour
{
    static bool isFinalWave;
    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
    static GameState gameState = GameState.Playing;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        isFinalWave = false;
        gameState = GameState.Playing;
    }
    public static bool _isFinalWave
    {
        get
        {
            return isFinalWave;
        }
        set
        {
            isFinalWave = value;
        }
    }

    public static GameState _gameState
    {
        get
        {
            return gameState;
        }
        set
        {
            if (gameState != value && gameState != GameState.Lost)
            {
                if (value == GameState.Won)
                {
                    FindAnyObjectByType<WinLoseManager>().Win();
                }
                if (value == GameState.Lost)
                {
                    FindAnyObjectByType<WinLoseManager>().Lose();
                }
            }
            gameState = value;
        }
    }
    private void Win()
    {
        StartCoroutine(WinCo());
    }
    private IEnumerator WinCo()
    {
        yield return new WaitForSeconds(1);
        SceneSwitcher.SwitchScene("WinScreen");
    }
    private void Lose()
    {
        StartCoroutine(LoseCo());
    }
    private IEnumerator LoseCo()
    {
        SFXManager sfx = FindAnyObjectByType<SFXManager>();
        sfx.PlayGlobalSoundFXClip(sfx.loseSound, 0.5f);
        yield return new WaitForSeconds((sfx.loseSound.length));
        SceneSwitcher.SwitchScene("LoseScreen");
    }
}
