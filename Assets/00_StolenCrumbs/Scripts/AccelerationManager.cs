using UnityEngine;
using TMPro;

public class AccelerationManager : MonoBehaviour
{
    TextMeshProUGUI numberText;
    void Start()
    {
        numberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeSpeed()
    {
        TimeScaleManager.SetTime((Time.timeScale + 1) % 4);
        if (Time.timeScale == 0) TimeScaleManager.SetTime(1);
        numberText.text = "X" + Time.timeScale;
    }
}
