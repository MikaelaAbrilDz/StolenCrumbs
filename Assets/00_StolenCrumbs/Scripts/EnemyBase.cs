using UnityEngine;

public class EnemyBase : CheckerPlaceable
{
    int maxLife;
    int currentLife;
    int damage;
    int killingPrice;
    GameObject visual;
    enum bugType
    {
        flyer, armored
    }
    bugType[] bugTypes;

    void HitBase()
    {

    }
    void GetDamaged(int damage)
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
