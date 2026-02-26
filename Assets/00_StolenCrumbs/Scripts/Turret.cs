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
	EnemyBase[] targetEnemies;
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
		EnemyBase target = FindTarget(parentChecker, range);
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

	EnemyBase FindTarget(CheckerManager initialChecker, int rangeToDo)
	{
		EnemyBase enemy = null;
		for (int i = 0; i < 6; i++)
		{
			enemy = initialChecker.sideCheckers[i].GetComponentInChildren<EnemyBase>();
			if (rangeToDo > 0)
			{
                EnemyBase newEnemy = FindTarget(initialChecker.sideCheckers[i], rangeToDo - 1);

				//FALTA CAMBIAR A LÓGICA DE DISTANCIA A LA BASE
				if (newEnemy != null)
				{
					return newEnemy;
				}
				if (enemy != null)
				{
					return enemy;
				}
			}
			else
			{
				return enemy;
			}
		}
		return enemy;
	}
	public void Sell()
	{

	}
}
