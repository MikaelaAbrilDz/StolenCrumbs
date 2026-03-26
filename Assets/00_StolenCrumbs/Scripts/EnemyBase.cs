using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TurretData;

public class EnemyBase : CheckerPlaceable
{
	int maxLife;
	[SerializeField] int currentLife = 40;
	int damage = 2;
	[SerializeField ] float movementSpeed = 1.0f;
	int killingPrice;
	GameObject visual;
	[HideInInspector] public List<TurretData.damageType> statusEffects = new List<TurretData.damageType>();
	[HideInInspector] public List<float> statusEffectsTimeLeft = new List<float>();
    [HideInInspector] public LTDescr currentTween;

	[SerializeField] GameObject explosionVisual;

	public enum bugType
	{
		flyer, armored
	}
	bugType[] bugTypes;
	private void OnEnable()
	{
		Invoke(nameof(MoveNextPath), 1.0f);
	}
    private void Update()
    {
		WearOffStatus();
    }
	private void WearOffStatus()
	{
		for (int i = 0; i < statusEffectsTimeLeft.Count; i++)
		{
			statusEffectsTimeLeft[i] -= Time.deltaTime;
			if (statusEffectsTimeLeft[i] <= 0)
			{
				statusEffectsTimeLeft.RemoveAt(i);
				statusEffects.RemoveAt(i);
			}
		}
	}
    public void GetStatusEffect(TurretData.damageType[] damageTypes, float[] damageTimes)
    {
		for (int i = 0; i < damageTypes.Length; i++)
		{
			statusEffects.Add(damageTypes[i]);
			statusEffectsTimeLeft.Add(damageTimes[i]);
        }
    }
    public void GetDamaged(int damage, TurretData.damageType[] damageTypes, GameObject hitVisual)
	{
		currentLife = Mathf.Max(currentLife - damage, 0);

		Instantiate(hitVisual, transform.position, Quaternion.identity);

		foreach (var damageType in damageTypes) //Checks synergies
		{
			foreach (var statusEffect in statusEffects) 
			{
				if (damageType == TurretData.damageType.fire && statusEffect == TurretData.damageType.gas)
				{
                    GetDamaged(10, new TurretData.damageType[1] { TurretData.damageType.explosion }, explosionVisual);
				}

            }
		}

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
