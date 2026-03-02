using UnityEngine;
using System.Collections;

public class EnemyBase : CheckerPlaceable
{
	int maxLife;
	int currentLife = 100;
	int damage = 2;
	[SerializeField ] float movementSpeed = 1.0f;
	int killingPrice;
	GameObject visual;
    [HideInInspector] public LTDescr currentTween;

	public enum bugType
	{
		flyer, armored
	}
	bugType[] bugTypes;
	private void OnEnable()
	{
		Invoke(nameof(MoveNextPath), 1.0f);
	}

	void HitBase()
	{

	}
	public void GetDamaged(int damage, Turret.damageType[] damageTypes)
	{
		currentLife = Mathf.Max(currentLife - damage, 0);
		print(name + " has " + currentLife + " HP now");
		if (currentLife == 0) Die();
	}
	void MoveNextPath()
	{
		Path path = parentChecker.GetComponentInChildren<Path>();
		if (path != null)
		{
			currentTween = LeanTween.move(gameObject, parentChecker.sideCheckers[path.direction].transform.position, 1 / movementSpeed).setOnComplete(MoveNextPath);
			StartCoroutine(PassNextChecker((1 / movementSpeed) / 2, parentChecker.sideCheckers[path.direction]));
		}
		Fort fort = parentChecker.GetComponentInChildren<Fort>();
		if (fort != null)
		{
			fort.GetDamaged(damage);
			Die();
		}

	}
	void Die()
	{
		Destroy(gameObject);
	}

	IEnumerator PassNextChecker(float delay, CheckerManager parent)
	{
		yield return new WaitForSeconds(delay);
		SetParentChecker(parent);
	}
}
