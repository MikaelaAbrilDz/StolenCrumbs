using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretData")]
public class TurretData : ScriptableObject
{
    public string turretName;
    public Sprite icon;
    public float bulletYield = 0.5f;
    public int fireRate = 1;
    public int damage = 10;
    public int range = 2;
    public GameObject bulletPrefab;
    public GameObject hitVisualPrefab;
    public int basePrice = 10;
    public enum damageType
    {
        gas, electric, water, fire, explosion, floor
    }
    public damageType[] damageTypes;
    public float[] damageTimes;

}
