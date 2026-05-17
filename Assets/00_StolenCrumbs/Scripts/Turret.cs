using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Turret : CheckerPlaceable
{
	[SerializeField] Transform canonPivot, shootPoint;

	public TurretData turretData;

    public int paidPrice;

	GameObject bullet;
	public List<EnemyBase> targetEnemies = new List<EnemyBase>();
	EnemyBase target;

	LTDescr tween;

	[SerializeField] AudioClip shootSound;
	[SerializeField] AudioClip hitSound;
    void Start()
	{
        bullet = Instantiate(turretData.bulletPrefab, shootPoint);
        bullet.SetActive(false);
		FindAnyObjectByType<TurretShooterManager>().turrets.Add(this);
	}
    private void Update()
    {
		if (target != null)
		{
			LookAtTarget();
			if (bullet.activeSelf) tween.to = target.transform.position;
		}
    }
	public void LookAtTarget()
	{
		float angleToLook = Mathf.Atan2(target.transform.position.y - canonPivot.transform.position.y, target.transform.position.x - canonPivot.transform.position.x) * Mathf.Rad2Deg;
		angleToLook = ((angleToLook % 360) + 360) % 360;

        if (angleToLook > 90 && angleToLook < 275)
		{
			canonPivot.transform.localScale = new Vector3(-1, 1, 1);
			canonPivot.transform.eulerAngles = new Vector3(0, 0, angleToLook + 180);
        }
		else
		{
			canonPivot.transform.localScale = new Vector3(1, 1, 1);
			canonPivot.transform.eulerAngles = new Vector3(0, 0, angleToLook);
		}
	}

    public void Shoot()
	{
        FindAnyObjectByType<SFXManager>().PlaySoundFXClip(shootSound, transform, 1f);
        target = FindTarget();
		if (target == null) return;
        StartCoroutine(DealDamage(target, turretData.bulletYield));
        bullet.SetActive(true);
		tween = LeanTween.move(bullet, target.transform.position, turretData.bulletYield);
	}
	private IEnumerator DealDamage(EnemyBase target, float delay)
	{
		yield return new WaitForSeconds(delay);
        bullet.SetActive(false);
        bullet.transform.localPosition = Vector3.zero;
		target.GetStatusEffect(turretData.damageTypes, turretData.damageTimes);
		yield return new WaitForEndOfFrame();
		if (target) target.GetDamaged(turretData.damage, turretData.damageTypes, turretData.hitVisualPrefab, hitSound);
	}

	EnemyBase FindTarget()
	{
		targetEnemies.Clear();
		FindTarget(parentChecker, turretData.range);
		if (targetEnemies.Count == 0) return null;
		EnemyBase bestSuitedEnemy = targetEnemies[0];
		for (int i = 1; i < targetEnemies.Count; i++)
		{
			if (targetEnemies[i].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort < bestSuitedEnemy.GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort)
			{
				bestSuitedEnemy = targetEnemies[i];
			}
			else if (targetEnemies[i].GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort == bestSuitedEnemy.GetParentChecker().GetComponentInChildren<Path>().stepsUntilFort)
			{
				float a, b;

				if (targetEnemies[i].currentTween.ratioPassed > 0.5) a = targetEnemies[i].currentTween.ratioPassed -1;
				else a = targetEnemies[i].currentTween.ratioPassed;
				if (bestSuitedEnemy.currentTween.ratioPassed > 0.5) b = bestSuitedEnemy.currentTween.ratioPassed - 1;
				else b = bestSuitedEnemy.currentTween.ratioPassed;


                if (a > b) bestSuitedEnemy = targetEnemies[i];
			}
        }
		return bestSuitedEnemy;
	}
	void FindTarget(CheckerManager initialChecker, int rangeToDo)
    {
		if (rangeToDo > 0)
		{
			for (int i = 0; i < 6; i++)
			{
				if (initialChecker.sideCheckers[i])
				{
					FindTarget(initialChecker.sideCheckers[i], rangeToDo - 1);
					EnemyBase[] targets = initialChecker.sideCheckers[i].GetComponentsInChildren<EnemyBase>();
				foreach (var target in targets)
				{
					if (!targetEnemies.Contains(target) && target != null && !target.isDead) targetEnemies.Add(target);
				}
                }
            }
		}
		else return;
	}
}
