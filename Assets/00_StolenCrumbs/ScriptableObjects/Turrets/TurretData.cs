using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject turretUiPrefab;
    public string turretName;
    public Sprite icon;
    public float bulletYield = 0.5f;
    public int fireRate = 1;
    public int damage = 10;
    public int range = 2;
    public GameObject bulletPrefab;
    public GameObject hitVisualPrefab;
    public int basePrice = 10;
    public int placedTurrets = 0;
    public float moneyMultiplier = 0.5f;
    public enum damageType
    {
        gas, electric, water, fire, sticky, explosion, floor
    }
    public damageType[] damageTypes;
    public float[] damageTimes;

    public int FinalPrice()
    {
        int finalPrice = basePrice;
        for (int i = 0 ; i < placedTurrets; i++)
        {
            finalPrice += (int)(finalPrice * moneyMultiplier);
        }
        return finalPrice;
    }
}
