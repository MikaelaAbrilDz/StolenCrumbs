using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Turret : CheckerPlaceable
{
	[SerializeField] GameObject bulletPrefab;
	float bulletYield = 0.5f;
	int fireRate = 1;
	int damage = 10;
	int range = 2;
	int paidPrice;
	public enum damageType
	{
		gas, electric, water, fire, floor
	}
	damageType[] damageTypes;

	GameObject bullet;
	float fireCooldown;
	public List<EnemyBase> targetEnemies = new List<EnemyBase>();
	void Start()
	{
		bullet = Instantiate(bulletPrefab, transform);
		bullet.SetActive(false);
	}

	void Update()
	{
		fireCooldown -= Time.deltaTime * fireRate;
		if (fireCooldown <= 0)
		{
			fireCooldown = 1;
			Shoot();
		}
	}

	public void Shoot()
	{
		EnemyBase target = FindTarget();
		if (target == null) return;
        StartCoroutine(DealDamage(target, bulletYield));
		bullet.SetActive(true);
		LeanTween.move(bullet, target.transform.position, bulletYield);
	}
	private IEnumerator DealDamage(EnemyBase target, float delay)
	{
		yield return new WaitForSeconds(delay);
		bullet.SetActive(false);
		bullet.transform.localPosition = Vector3.zero;
		target.GetDamaged(damage, damageTypes);
	}

	EnemyBase FindTarget()
	{
		targetEnemies.Clear();
		FindTarget(parentChecker, range);
		if (targetEnemies.Count == 0) return null;
		EnemyBase bestSuitedEnemy = targetEnemies[0];
		for (int i = 0; i < targetEnemies.Count - 1; i++)
		{
			if (targetEnemies[i].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort < targetEnemies[i + 1].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort)
			{
				bestSuitedEnemy = targetEnemies[i];
			}
			else if (targetEnemies[i].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort > targetEnemies[i + 1].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort) bestSuitedEnemy = targetEnemies[i + 1];
			else
			{
				float a, b;

				if (targetEnemies[i].currentTween.ratioPassed > 0.5) a = 1 - targetEnemies[i].currentTween.ratioPassed;
				else a = targetEnemies[i].currentTween.ratioPassed;
				if (targetEnemies[i + 1].currentTween.ratioPassed > 0.5) b = 1 - targetEnemies[i + 1].currentTween.ratioPassed;
				else b = targetEnemies[i + 1].currentTween.ratioPassed;

				if (a > b) bestSuitedEnemy = targetEnemies[i];
				else bestSuitedEnemy = targetEnemies[i + 1];
			}

        }
		print(bestSuitedEnemy);
		return bestSuitedEnemy;
	}
	void FindTarget(CheckerManager initialChecker, int rangeToDo)
    {
		if (rangeToDo > 0)
		{
			for (int i = 0; i < 6; i++)
			{
				FindTarget(initialChecker.sideCheckers[i], rangeToDo - 1);
				EnemyBase[] targets = initialChecker.sideCheckers[i].GetComponentsInChildren<EnemyBase>();
				foreach (var target in targets)
				{
					if (!targetEnemies.Contains(target) && target != null) targetEnemies.Add(target);
				}
			}
		}
		else return;
	}
	public void Sell()
	{

	}
}
