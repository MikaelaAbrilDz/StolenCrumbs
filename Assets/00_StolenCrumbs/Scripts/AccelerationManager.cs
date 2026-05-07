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
        Time.timeScale = (Time.timeScale + 1) % 4;
        if (Time.timeScale == 0) Time.timeScale = 1;
        numberText.text = "X" + Time.timeScale;
    }
}
