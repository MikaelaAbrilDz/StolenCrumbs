using UnityEngine;
using System.Collections;

public class EnemyBase : CheckerPlaceable
{
    int maxLife;
    int currentLife;
    int damage;
    float movementSpeed = 1.0f;
    int killingPrice;
    GameObject visual;

    public enum bugType
    {
        flyer, armored
    }
    bugType[] bugTypes;
	private void OnEnable()
	{
		Invoke(nameof(MoveNextPath),1.0f);
	}

	void HitBase()
    {

    }
    public void GetDamaged(int damage, Turret.damageType[] damageTypes)
    {
        currentLife -= damage;
    }
    void MoveNextPath()
    {
        Path path = parentChecker.GetComponentInChildren<Path>();
        if (path != null)
        {
            LeanTween.move(gameObject, parentChecker.sideCheckers[path.direction].transform.position,1/movementSpeed).setOnComplete(MoveNextPath);
            StartCoroutine(PassNextChecker((1/movementSpeed)/2, parentChecker.sideCheckers[path.direction]));
        }

    }
    void Die()
    {

    }

    IEnumerator PassNextChecker(float delay, CheckerManager parent)
    {
        yield return new WaitForSeconds(delay);
        SetParentChecker(parent);
    }
}
