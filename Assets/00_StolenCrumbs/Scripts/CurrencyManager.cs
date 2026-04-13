using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    static int currency = 60;
    public static void AddCurrency(int amount)
    {
        currency += amount;
    }
    public static bool RemoveCurrency(int amount)
    {
        if (amount > currency) return false;
        else
        {
            currency -= amount;
            return true;
        }
    }
    public static void SetCurrency(int value)
    {
        currency = value;
    }
    public static int Currency()
    {
        return currency;
    }
}
