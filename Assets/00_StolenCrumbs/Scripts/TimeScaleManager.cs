using UnityEngine;

public abstract class TimeScaleManager : MonoBehaviour
{
    static float scale = 1;

    public static void PauseTime()
    {
        scale = Time.timeScale;
        Time.timeScale = 0;
    }
    public static void UnPauseTime()
    {
        Time.timeScale = scale;
    }
    public static void SetTime(float scaleToPut)
    {
        Time.timeScale = scaleToPut;
        scale = Time.timeScale;
    }
}
