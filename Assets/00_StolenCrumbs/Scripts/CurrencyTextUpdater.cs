using UnityEngine;
using TMPro;

public class CurrencyTextUpdater : MonoBehaviour
{
    TextMeshProUGUI currencyText;
    void Start()
    {
        currencyText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        currencyText.text = CurrencyManager.Currency() + " bolts";
    }
}
