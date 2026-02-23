using UnityEngine;

public class EnemyBase : CheckerPlaceable
{
    int maxLife;
    int currentLife;
    int damage;
    int killingPrice;
    GameObject visual;
    public enum bugType
    {
        flyer, armored
    }
    bugType[] bugTypes;

    void HitBase()
    {

    }
    public void GetDamaged(int damage, Turret.damageType[] damageTypes)
    {
        currentLife -= damage;
    }
    void MoveNextPath()
    {

    }
    void Die()
    {

    }
}
